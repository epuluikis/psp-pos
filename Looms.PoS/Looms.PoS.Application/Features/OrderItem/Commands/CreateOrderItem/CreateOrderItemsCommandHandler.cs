using Looms.PoS.Application.Features.Discount.Commands.CreateDiscount;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
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
    private readonly IProductUpdatesService _productUpdatesService;
    private readonly IProductVariationUpdatesService _productVariationUpdatesService;
    private readonly IReservationsRepository _reservationsRepository;
    private readonly IReservationModelsResolver _reservationModelsResolver;

    public CreateOrderItemsCommandHandler(IHttpContentResolver httpContentResolver,
        IOrderItemsRepository orderItemsRepository,
        IOrderItemModelsResolver modelsResolver,
        IOrderModelsResolver orderModelsResolver,
        IOrdersRepository ordersRepository,
        IProductsRepository productsRepository,
        IProductVariationRepository variationsRepository,
        IProductUpdatesService productUpdatesService,
        IProductVariationUpdatesService productVariationUpdatesService,
        IReservationsRepository reservationsRepository,
        IReservationModelsResolver reservationModelsResolver)
    {
        _httpContentResolver = httpContentResolver;
        _orderItemsRepository = orderItemsRepository;
        _modelsResolver = modelsResolver;
        _orderModelsResolver = orderModelsResolver;
        _ordersRepository = ordersRepository;
        _productsRepository = productsRepository;
        _variationsRepository = variationsRepository;
        _productUpdatesService = productUpdatesService;
        _productVariationUpdatesService = productVariationUpdatesService;
        _reservationsRepository = reservationsRepository;
        _reservationModelsResolver = reservationModelsResolver;
    }

    public async Task<IActionResult> Handle(CreateOrderItemsCommand command, CancellationToken cancellationToken)
    {
        var orderItemRequest = await _httpContentResolver.GetPayloadAsync<CreateOrderItemRequest>(command.Request);
        var orderId = Guid.Parse(command.OrderId);

        var product = orderItemRequest.ProductId is null ? null : await _productsRepository.GetAsync(Guid.Parse(orderItemRequest.ProductId));
        var productVariation = orderItemRequest.ProductVariationId is null ? null : await _variationsRepository.GetAsync(Guid.Parse(orderItemRequest.ProductVariationId));
        var reservation = orderItemRequest.ReservationId is null ? null : await _reservationsRepository.GetAsync(Guid.Parse(orderItemRequest.ReservationId));
        
        var orderItemDao = _modelsResolver.GetDaoFromRequest(orderId, orderItemRequest, product, productVariation, reservation);

        await CompleteTransaction(product, productVariation, reservation, orderItemDao, orderItemRequest.Quantity);

        var orderDao = await _ordersRepository.GetAsync(orderId);
        var response = _orderModelsResolver.GetResponseFromDao(orderDao);

        return new OkObjectResult(response); 
    }

    private async Task CompleteTransaction(ProductDao product, ProductVariationDao productVariation, ReservationDao reservationDao, OrderItemDao orderItemDao ,int quantity)
    {
        using TransactionScope tran = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        if (product is not null)
        {
            await _productUpdatesService.UpdateProductStock(product, quantity);
        }
        if (productVariation is not null)
        {
            await _productVariationUpdatesService.UpdateProductVariationStock(productVariation, quantity);
        }
        if(reservationDao is not null)
        {
            var reservation = _reservationModelsResolver.GetUpdatedStatusDao(reservationDao, ReservationStatus.Ongoing);
            await _reservationsRepository.UpdateAsync(reservationDao);
        }
        await _orderItemsRepository.CreateAsync(orderItemDao);
        tran.Complete();
    }
}