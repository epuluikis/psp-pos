using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace Looms.PoS.Application.Features.Order.Commands.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, IActionResult>
{
    private readonly IOrdersRepository _orderRepository;
    private readonly IOrderModelsResolver _orderModelsResolver;
    private readonly IOrderService _orderService;

    public DeleteOrderCommandHandler(
        IOrdersRepository orderRepository,
        IOrderModelsResolver orderModelsResolver,
        IOrderService orderService
    )
    {
        _orderRepository = orderRepository;
        _orderModelsResolver = orderModelsResolver;
        _orderService = orderService;
    }

    public async Task<IActionResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var originalDao = await _orderRepository.GetAsync(Guid.Parse(request.Id));

        var orderDao = _orderModelsResolver.GetDaoFromDaoAndStatus(originalDao, OrderStatus.Cancelled);


        using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _orderRepository.UpdateAsync(orderDao);
            await _orderService.ResetQuantity(orderDao);

            transactionScope.Complete();
        }

        return new NoContentResult();
    }
}
