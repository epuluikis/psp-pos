using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.Factories;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Application.Models.Requests.Payment;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Payment.Commands.CreatePayment;

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, IActionResult>
{
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IPaymentModelsResolver _paymentModelsResolver;
    private readonly IPaymentHandlerServiceFactory _paymentHandlerServiceFactory;
    private readonly IOrdersRepository _ordersRepository;
    private readonly IOrderService _orderService;
    private readonly IOrderModelsResolver _orderModelsResolver;

    public CreatePaymentCommandHandler(
        IHttpContentResolver httpContentResolver,
        IPaymentModelsResolver paymentModelsResolver,
        IPaymentHandlerServiceFactory paymentHandlerServiceFactory,
        IOrdersRepository ordersRepository,
        IOrderService orderService,
        IOrderModelsResolver orderModelsResolver
    )
    {
        _httpContentResolver = httpContentResolver;
        _paymentModelsResolver = paymentModelsResolver;
        _paymentHandlerServiceFactory = paymentHandlerServiceFactory;
        _ordersRepository = ordersRepository;
        _orderService = orderService;
        _orderModelsResolver = orderModelsResolver;
    }

    public async Task<IActionResult> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
    {
        var paymentRequest = await _httpContentResolver.GetPayloadAsync<CreatePaymentRequest>(command.Request);
        var paymentDao = _paymentModelsResolver.GetDaoFromRequest(paymentRequest);

        await UpdateOrderBefore(paymentDao);

        paymentDao = await _paymentHandlerServiceFactory.GetService(paymentDao.PaymentMethod).HandlePayment(paymentDao);

        await UpdateOrderAfter(paymentDao);

        var response = _paymentModelsResolver.GetResponseFromDao(paymentDao);

        return new CreatedAtRouteResult($"/payments/{paymentDao.Id}", response);
    }

    private async Task UpdateOrderBefore(PaymentDao paymentDao)
    {
        var orderDao = await _ordersRepository.GetAsync(paymentDao.OrderId);

        if (orderDao.Status is not OrderStatus.Pending)
        {
            return;
        }

        orderDao = _orderModelsResolver.GetDaoFromDaoAndStatus(orderDao, OrderStatus.Locked);
        await _ordersRepository.UpdateAsync(orderDao);
    }

    private async Task UpdateOrderAfter(PaymentDao paymentDao)
    {
        if (paymentDao.Status is not PaymentStatus.Succeeded)
        {
            return;
        }

        var orderDao = await _ordersRepository.GetAsync(paymentDao.OrderId);
        var payableAmount = _orderService.CalculatePayableAmount(orderDao);

        if (payableAmount is not 0)
        {
            return;
        }

        orderDao = _orderModelsResolver.GetDaoFromDaoAndStatus(orderDao, OrderStatus.Paid);
        await _ordersRepository.UpdateAsync(orderDao);
    }
}
