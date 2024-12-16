using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace Looms.PoS.Application.Features.OrderItem.Commands.DeleteOrderItem;

public class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand, IActionResult>
{
    private readonly IOrderItemsRepository _orderItemsRepository;
    private readonly IOrderItemModelsResolver _modelsResolver;
    private readonly IProductUpdatesService _productUpdatesService;
    private readonly IProductVariationUpdatesService _productVariationUpdatesService;
    private readonly IReservationModelsResolver _reservationModelsResolver;
    private readonly IReservationsRepository _reservationsRepository;

    public DeleteOrderItemCommandHandler(IOrderItemsRepository orderItemsRepository, 
        IOrderItemModelsResolver modelsResolver,
        IProductUpdatesService productUpdatesService,
        IProductVariationUpdatesService productVariationUpdatesService,
        IReservationModelsResolver reservationModelsResolver,
        IReservationsRepository reservationsRepository)
    {
        _orderItemsRepository = orderItemsRepository;
        _modelsResolver = modelsResolver;
        _productUpdatesService = productUpdatesService;
        _productVariationUpdatesService = productVariationUpdatesService;
        _reservationModelsResolver = reservationModelsResolver;
        _reservationsRepository = reservationsRepository;
    }

    public async Task<IActionResult> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
    {
        var originalDao = await _orderItemsRepository.GetAsync(Guid.Parse(request.Id));

        await CompleteTransaction(originalDao);
        
        return new NoContentResult();
    }

    private async Task CompleteTransaction(OrderItemDao orderItemDao)
    {
        using TransactionScope tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        if (orderItemDao.Product is not null)
        {
            await _productUpdatesService.UpdateProductStock(orderItemDao.Product, -orderItemDao.Quantity);
        }

        if (orderItemDao.ProductVariation is not null)
        {
            await _productVariationUpdatesService.UpdateProductVariationStock(orderItemDao.ProductVariation, -orderItemDao.Quantity);
        }

        var deleteItemDao = _modelsResolver.GetDeletedDao(orderItemDao);
        await _orderItemsRepository.UpdateAsync(deleteItemDao);

        tran.Complete();

    }
}