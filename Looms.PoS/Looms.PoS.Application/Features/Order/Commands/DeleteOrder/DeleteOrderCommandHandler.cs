using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace Looms.PoS.Application.Features.Order.Commands.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, IActionResult>
{
    private readonly IOrdersRepository _orderRepository;
    private readonly IOrderItemsRepository _orderItemRepository;
    private readonly IOrderItemModelsResolver _orderItemModelResolver;
    private readonly IOrderModelsResolver _orderModelsResolver;
    private readonly IProductUpdatesService _productUpdatesService;
    private readonly IProductVariationUpdatesService _productVariationUpdatesService;

    public DeleteOrderCommandHandler(IOrdersRepository orderRepository, 
        IOrderItemsRepository orderItemRepository, 
        IOrderItemModelsResolver orderItemModelResolver,
        IOrderModelsResolver orderModelsResolver,
        IProductUpdatesService productUpdatesService,
        IProductVariationUpdatesService productVariationUpdatesService)
    {
        _orderRepository = orderRepository;
        _orderItemRepository = orderItemRepository;
        _orderItemModelResolver = orderItemModelResolver;
        _orderModelsResolver = orderModelsResolver;
        _productUpdatesService = productUpdatesService;
        _productVariationUpdatesService = productVariationUpdatesService;
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

        using TransactionScope tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        if(orderDao.OrderItems.Count != 0)
        {
            foreach (var orderItem in orderDao.OrderItems)
            {
                tasks.Add(DeleteRelatedItems(orderItem));
                var deleteItemDao = _orderItemModelResolver.GetDeletedDao(orderItem);
                var deleteItem = _orderItemRepository.UpdateAsync(deleteItemDao);
                tasks.Add(deleteItem);
            }
            await Task.WhenAll(tasks);
        }

        tran.Complete();
    }

    private async Task DeleteRelatedItems(OrderItemDao orderItem)
    {
        if(orderItem.Product is not null)
        {
            await _productUpdatesService.UpdateProductStock(orderItem.Product, -orderItem.Quantity);
        }
        if(orderItem.ProductVariation is not null)
        {
            await _productVariationUpdatesService.UpdateProductVariationStock(orderItem.ProductVariation, -orderItem.Quantity);
        }
    }

}