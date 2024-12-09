using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Order.Commands.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, IActionResult>
{
    private readonly IOrdersRepository _orderRepository;
    private readonly IOrderItemsRepository _orderItemRepository;
    private readonly IOrderItemModelsResolver _orderItemModelResolver;
    private readonly IOrderModelsResolver _orderModelsResolver;

    public DeleteOrderCommandHandler(IOrdersRepository orderRepository, 
        IOrderItemsRepository orderItemRepository, 
        IOrderItemModelsResolver orderItemModelResolver,
        IOrderModelsResolver orderModelsResolver)
    {
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _orderItemModelResolver = orderItemModelResolver;
        _orderModelsResolver = orderModelsResolver;
    }
    
    public async Task<IActionResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var originalDao = await _orderRepository.GetAsync(Guid.Parse(request.Id));

        var orderDao = _orderModelsResolver.GetDeletedDao(originalDao);
        _ = await _orderRepository.UpdateAsync(orderDao);

        if(orderDao.OrderItems.Count != 0)
        {
            foreach (var orderItem in orderDao.OrderItems)
            {
                var originalOrderItemDao = await _orderItemRepository.GetAsync(orderItem.Id);
                var orderItemDao = _orderItemModelResolver.GetDeletedDao(originalOrderItemDao);
                _ = await _orderItemRepository.UpdateAsync(orderItemDao);
            }
        }
        return new NoContentResult();
    }
}