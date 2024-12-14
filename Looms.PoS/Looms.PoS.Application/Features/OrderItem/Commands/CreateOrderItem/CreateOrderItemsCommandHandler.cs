using FluentValidation.Validators;
using Looms.PoS.Application.Features.Discount.Commands.CreateDiscount;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

// TODO: Add checks for service availability, if service is not being ordered when it is reserved

namespace Looms.PoS.Application.Features.OrderItem.Commands.CreateOrderItem;

public record CreateOrderItemsCommandHandler : IRequestHandler<CreateOrderItemsCommand, IActionResult>
{
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IOrderModelsResolver _orderModelsResolver;
    private readonly IOrderItemModelsResolver _modelsResolver;
    private readonly IOrderItemsRepository _orderItemsRepository;
    private readonly IOrdersRepository _ordersRepository;
    private readonly IProductsRepository _productsRepository;
    private readonly IProductVariationRepository _variationsRepository;
    private readonly IProductModelsResolver _productModelsResolver;
    private readonly IProductVariationModelsResolver _productVariationModelsResolver;

    public CreateOrderItemsCommandHandler(IHttpContentResolver httpContentResolver,
        IOrderItemsRepository orderItemsRepository,
        IOrderItemModelsResolver modelsResolver,
        IOrderModelsResolver orderModelsResolver,
        IOrdersRepository ordersRepository,
        IProductsRepository productsRepository,
        IProductVariationRepository variationsRepository,
        IProductModelsResolver productModelsResolver,
        IProductVariationModelsResolver productVariationModelsResolver)
    {
        _httpContentResolver = httpContentResolver;
        _orderItemsRepository = orderItemsRepository;
        _modelsResolver = modelsResolver;
        _orderModelsResolver = orderModelsResolver;
        _ordersRepository = ordersRepository;
        _productsRepository = productsRepository;
        _variationsRepository = variationsRepository;
        _productModelsResolver = productModelsResolver;
        _productVariationModelsResolver = productVariationModelsResolver;
    }

    public async Task<IActionResult> Handle(CreateOrderItemsCommand command, CancellationToken cancellationToken)
    {
        var orderItemRequest = await _httpContentResolver.GetPayloadAsync<CreateOrderItemRequest>(command.Request);
        var orderId = Guid.Parse(command.OrderId);

        var product = orderItemRequest.ProductId is null ? null : await _productsRepository.GetAsync(Guid.Parse(orderItemRequest.ProductId));
        var productVariation = orderItemRequest.ProductVariationId is null ? null : await _variationsRepository.GetAsync(Guid.Parse(orderItemRequest.ProductVariationId));
        
        var orderItemDao = _modelsResolver.GetDaoFromRequest(orderId, orderItemRequest, product, productVariation);

        await CompleteTransaction(product, productVariation, orderItemDao, orderItemRequest.Quantity);

        var orderDao = await _ordersRepository.GetAsync(orderId);
        var response = _orderModelsResolver.GetResponseFromDao(orderDao);

        return new OkObjectResult(response); 
    }

    private async Task CompleteTransaction(ProductDao product, ProductVariationDao productVariation, OrderItemDao orderItemDao ,int quantity)
    {
        using TransactionScope tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        if (product != null)
        {
            var updatedProductDao = _productModelsResolver.GetUpdatedQuantityDao(product, quantity);
            await _productsRepository.UpdateAsync(updatedProductDao);
        }
        if (productVariation != null)
        {
            var updatedVariationDao = _productVariationModelsResolver.GetUpdatedQuantityDao(productVariation, quantity);
            await _variationsRepository.UpdateAsync(updatedVariationDao);
        }
        await _orderItemsRepository.CreateAsync(orderItemDao);
        tran.Complete();
    }
}