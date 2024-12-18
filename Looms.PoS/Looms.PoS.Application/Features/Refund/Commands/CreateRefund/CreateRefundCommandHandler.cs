using Looms.PoS.Application.Helpers;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.Factories;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Refund;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Refund.Commands.CreateRefund;

public class CreateRefundCommandHandler : IRequestHandler<CreateRefundCommand, IActionResult>
{
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IRefundModelsResolver _refundModelsResolver;
    private readonly IOrdersRepository _ordersRepository;
    private readonly IOrderModelsResolver _orderModelsResolver;
    private readonly IPaymentsRepository _paymentsRepository;
    private readonly IRefundHandlerServiceFactory _refundHandlerServiceFactory;

    public CreateRefundCommandHandler(
        IHttpContentResolver httpContentResolver,
        IRefundModelsResolver refundModelsResolver,
        IOrdersRepository ordersRepository,
        IOrderModelsResolver orderModelsResolver,
        IPaymentsRepository paymentsRepository,
        IRefundHandlerServiceFactory refundHandlerServiceFactory
    )
    {
        _httpContentResolver = httpContentResolver;
        _refundModelsResolver = refundModelsResolver;
        _ordersRepository = ordersRepository;
        _orderModelsResolver = orderModelsResolver;
        _paymentsRepository = paymentsRepository;
        _refundHandlerServiceFactory = refundHandlerServiceFactory;
    }

    public async Task<IActionResult> Handle(CreateRefundCommand command, CancellationToken cancellationToken)
    {
        var refundRequest = await _httpContentResolver.GetPayloadAsync<CreateRefundRequest>(command.Request);
        var paymentDao = await _paymentsRepository.GetAsync(Guid.Parse(refundRequest.PaymentId));
        var refundDao = _refundModelsResolver.GetDaoFromRequest(
            refundRequest,
            Guid.Parse(HttpContextHelper.GetUserId(command.Request)),
            paymentDao
        );

        refundDao = await _refundHandlerServiceFactory.GetService(paymentDao.PaymentMethod).HandleRefund(refundDao);

        await UpdateOrder(refundDao);

        var response = _refundModelsResolver.GetResponseFromDao(refundDao);

        return new CreatedAtRouteResult($"/refunds/{refundDao.Id}", response);
    }

    private async Task UpdateOrder(RefundDao refundDao)
    {
        if (refundDao.Status is not (RefundStatus.Pending or RefundStatus.Completed))
        {
            return;
        }

        var orderDao = await _ordersRepository.GetAsync(refundDao.OrderId);

        if (orderDao.Status is OrderStatus.Refunded)
        {
            return;
        }

        orderDao = _orderModelsResolver.GetDaoFromDaoAndStatus(orderDao, OrderStatus.Refunded);
        await _ordersRepository.UpdateAsync(orderDao);
    }
}
