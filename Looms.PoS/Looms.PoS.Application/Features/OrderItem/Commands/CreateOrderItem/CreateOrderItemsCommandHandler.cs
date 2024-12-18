using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Application.Models.Requests.OrderItem;
using Looms.PoS.Application.Services;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace Looms.PoS.Application.Features.OrderItem.Commands.CreateOrderItem;

public record CreateOrderItemsCommandHandler : IRequestHandler<CreateOrderItemsCommand, IActionResult>
{
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IOrderItemModelsResolver _orderItemModelsResolver;
    private readonly IOrderItemsRepository _orderItemsRepository;
    private readonly IProductsRepository _productsRepository;
    private readonly IProductVariationRepository _variationsRepository;
    private readonly IServicesRepository _servicesRepository;
    private readonly IOrderItemService _orderItemService;

    public CreateOrderItemsCommandHandler(
        IHttpContentResolver httpContentResolver,
        IOrderItemsRepository orderItemsRepository,
        IOrderItemModelsResolver orderItemModelsResolver,
        IProductsRepository productsRepository,
        IProductVariationRepository variationsRepository,
        IProductService productService,
        IProductVariationService productVariationService,
        IServicesRepository servicesRepository,
        IOrderItemService orderItemService
    )
    {
        _httpContentResolver = httpContentResolver;
        _orderItemsRepository = orderItemsRepository;
        _orderItemModelsResolver = orderItemModelsResolver;
        _productsRepository = productsRepository;
        _variationsRepository = variationsRepository;
        _servicesRepository = servicesRepository;
        _orderItemService = orderItemService;
    }

    public async Task<IActionResult> Handle(CreateOrderItemsCommand command, CancellationToken cancellationToken)
    {
        var orderItemRequest = await _httpContentResolver.GetPayloadAsync<CreateOrderItemRequest>(command.Request);
        var orderId = Guid.Parse(command.OrderId);

        var product = orderItemRequest.ProductId is null ? null : await _productsRepository.GetAsync(Guid.Parse(orderItemRequest.ProductId));
        var productVariation = orderItemRequest.ProductVariationId is null
            ? null
            : await _variationsRepository.GetAsync(Guid.Parse(orderItemRequest.ProductVariationId));

        var service = orderItemRequest.ServiceId is null ? null : await _servicesRepository.GetAsync(Guid.Parse(orderItemRequest.ServiceId));

        var orderItemDao = _orderItemModelsResolver.GetDaoFromRequest(orderItemRequest, orderId, product, productVariation, service);

        using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _orderItemsRepository.CreateAsync(orderItemDao);
            await _orderItemService.SetQuantity(orderItemDao);

            transactionScope.Complete();
        }

        var response = _orderItemModelsResolver.GetResponseFromDao(orderItemDao);

        return new OkObjectResult(response);
    }
}
