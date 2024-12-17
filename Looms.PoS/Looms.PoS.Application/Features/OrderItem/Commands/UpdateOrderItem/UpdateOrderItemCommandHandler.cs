using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Requests.OrderItem;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

// TODO: Add rollback if a different product variation is added to update the quantity of the previous product variation

namespace Looms.PoS.Application.Features.OrderItem.Commands.UpdateOrderItem;

public class UpdateOrderItemCommandHandler : IRequestHandler<UpdateOrderItemCommand, IActionResult>
{
    private readonly IOrderItemsRepository _orderItemsRepository;
    private readonly IDiscountsRepository _discountsRepository;
    private readonly IOrderItemModelsResolver _modelsResolver;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IProductVariationUpdatesService _productVariationUpdatesService;
    private readonly IProductUpdatesService _productUpdatesService;

    public UpdateOrderItemCommandHandler(
        IOrderItemsRepository orderItemsRepository,
        IDiscountsRepository discountsRepository,
        IOrderItemModelsResolver modelsResolver,
        IHttpContentResolver httpContentResolver,
        IProductVariationUpdatesService productVariationUpdatesService,
        IProductUpdatesService productUpdatesService)
    {
        _orderItemsRepository = orderItemsRepository;
        _discountsRepository = discountsRepository;
        _modelsResolver = modelsResolver;
        _httpContentResolver = httpContentResolver;
        _productVariationUpdatesService = productVariationUpdatesService;
        _productUpdatesService = productUpdatesService;
    }

    public async Task<IActionResult> Handle(UpdateOrderItemCommand command, CancellationToken cancellationToken)
    {
        var orderItemRequest = await _httpContentResolver.GetPayloadAsync<UpdateOrderItemRequest>(command.Request);
        var originalDao = await _orderItemsRepository.GetAsync(Guid.Parse(command.Id));

        var discountDao = orderItemRequest.DiscountId is not null
            ? await _discountsRepository.GetAsync(Guid.Parse(orderItemRequest.DiscountId))
            : null;

        var orderItemDao = _modelsResolver.GetDaoFromDaoAndRequest(originalDao, orderItemRequest, discountDao);

        await CompleteTransaction(orderItemDao, originalDao);

        var response = _modelsResolver.GetResponseFromDao(orderItemDao);
        return new OkObjectResult(response);
    }

    private async Task CompleteTransaction(OrderItemDao orderItemDao, OrderItemDao originalDao)
    {
        var quantityToDeduct = orderItemDao.Quantity - originalDao.Quantity;

        using var tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        if (orderItemDao.Product is not null && quantityToDeduct != 0)
        {
            await _productUpdatesService.UpdateProductStock(orderItemDao.Product, quantityToDeduct);
        }

        if (originalDao.ProductVariation is not null)
        {
            if (originalDao.ProductVariation.Id == orderItemDao.ProductVariation.Id)
            {
                await _productVariationUpdatesService.UpdateProductVariationStock(originalDao.ProductVariation, quantityToDeduct);
            }
            else
            {
                // Rolling back quantity updates for the previous product variation
                var previousQuantity = -originalDao.Quantity;
                await _productVariationUpdatesService.UpdateProductVariationStock(originalDao.ProductVariation, previousQuantity);
            }
        }
        else if (orderItemDao.ProductVariation is not null)
        {
            // Updating the quantity for the new product variation
            await _productVariationUpdatesService.UpdateProductVariationStock(orderItemDao.ProductVariation, orderItemDao.Quantity);
        }

        await _orderItemsRepository.UpdateAsync(orderItemDao);
        tran.Complete();
    }
}
