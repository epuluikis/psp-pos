using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Daos;
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
        await _orderRepository.UpdateAsync(orderDao);

        await DeleteOrderItems(orderDao);
        return new NoContentResult();
    }

    private async Task DeleteOrderItems(OrderDao orderDao)
    {
        List<Task> tasks = [];


        if(orderDao.OrderItems.Count != 0)
        {
            foreach (var orderItem in orderDao.OrderItems)
            {
                var deleteItemDao = _orderItemModelResolver.GetDeletedDao(orderItem);
                var deleteItem = _orderItemRepository.UpdateAsync(deleteItemDao);
                tasks.Add(deleteItem);
            }
            await Task.WhenAll(tasks);
        }
    }
}