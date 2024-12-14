using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Exceptions;
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
    private readonly IProductsRepository _productsRepository;
    private readonly IProductVariationRepository _variationsRepository;
    private readonly IProductModelsResolver _productModelsResolver;
    private readonly IProductVariationModelsResolver _productVariationModelsResolver;

    public UpdateOrderItemCommandHandler(IOrderItemsRepository orderItemsRepository, 
        IDiscountsRepository discountsRepository,
        IOrderItemModelsResolver modelsResolver,
        IHttpContentResolver httpContentResolver,
        IProductModelsResolver productModelsResolver,
        IProductVariationModelsResolver productVariationModelsResolver,
        IProductsRepository productsRepository,
        IProductVariationRepository variationsRepository)
    {
        _orderItemsRepository = orderItemsRepository;
        _discountsRepository = discountsRepository;
        _modelsResolver = modelsResolver;
        _httpContentResolver = httpContentResolver;
        _productsRepository = productsRepository;
        _variationsRepository = variationsRepository;
        _productModelsResolver = productModelsResolver;
        _productVariationModelsResolver = productVariationModelsResolver;
    }

    public async Task<IActionResult> Handle(UpdateOrderItemCommand command, CancellationToken cancellationToken)
    {
        var orderItemRequest = await _httpContentResolver.GetPayloadAsync<UpdateOrderItemRequest>(command.Request);
        var originalDao = await _orderItemsRepository.GetAsync(Guid.Parse(command.Id));
        var productVariation = orderItemRequest.VariationId is null ? null : await _variationsRepository.GetAsync(Guid.Parse(orderItemRequest.VariationId));
        var quantityToDeduct = orderItemRequest.Quantity - originalDao.Quantity;

        if (quantityToDeduct != 0 && originalDao.Product.Quantity < quantityToDeduct)
        {
            throw new LoomsBadRequestException("Product quantity is too low.");
        }

        if(originalDao.ProductVariation != null && originalDao.ProductVariation.Quantity < quantityToDeduct)
        {
            throw new LoomsBadRequestException("Product variation quantity is too low.");
        } else if (productVariation != null && productVariation.Quantity < orderItemRequest.Quantity)
        {
            throw new LoomsBadRequestException("Product variation quantity is too low.");
        }

        var discountDao = orderItemRequest.DiscountId != null ? await _discountsRepository.GetAsync(Guid.Parse(orderItemRequest.DiscountId)) : null;
        var orderItemDao = _modelsResolver.GetDaoFromDaoAndRequest(originalDao, orderItemRequest, discountDao);

        await CompleteTransaction(orderItemDao, originalDao);

        var response = _modelsResolver.GetResponseFromDao(orderItemDao);
        return new OkObjectResult(response);
    }

    private async Task CompleteTransaction(OrderItemDao orderItemDao, OrderItemDao originalDao)
    {
        var quantityToDeduct = orderItemDao.Quantity - originalDao.Quantity;

        using TransactionScope tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        if (orderItemDao.Product != null && quantityToDeduct != 0)
        {
            var updatedProductDao = _productModelsResolver.GetUpdatedQuantityDao(orderItemDao.Product, quantityToDeduct);
            await _productsRepository.UpdateAsync(updatedProductDao);
        }

        if(originalDao.ProductVariation != null)
        {
            if(originalDao.ProductVariation.Id == orderItemDao.ProductVariation.Id)
            {
                var updatedOriginalVariationDao = _productVariationModelsResolver.GetUpdatedQuantityDao(originalDao.ProductVariation, quantityToDeduct);
                await _variationsRepository.UpdateAsync(updatedOriginalVariationDao);
            }else {
                var previousQuantity = -originalDao.Quantity;

                // Rolling back quantity updates for the previous product variation
                var updatedOriginalVariationDao = _productVariationModelsResolver.GetUpdatedQuantityDao(originalDao.ProductVariation, previousQuantity);
                await _variationsRepository.UpdateAsync(updatedOriginalVariationDao);
            }
        }else if (orderItemDao.ProductVariation != null)
        {
            // Updating the quantity for the new product variation
            var updatedNewVariationDao = _productVariationModelsResolver.GetUpdatedQuantityDao(orderItemDao.ProductVariation, orderItemDao.Quantity);
            await _variationsRepository.UpdateAsync(updatedNewVariationDao);
        }

        await _orderItemsRepository.UpdateAsync(orderItemDao);
        tran.Complete();
    }
}