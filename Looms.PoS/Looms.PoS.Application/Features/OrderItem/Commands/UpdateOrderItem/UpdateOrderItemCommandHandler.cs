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

namespace Looms.PoS.Application.Features.OrderItem.Commands.UpdateOrderItem;

public class UpdateOrderItemCommandHandler : IRequestHandler<UpdateOrderItemCommand, IActionResult>
{
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IOrderModelsResolver _orderModelsResolver;
    private readonly IOrderItemModelsResolver _orderItemModelsResolver;
    private readonly IOrderItemsRepository _orderItemsRepository;
    private readonly IOrdersRepository _ordersRepository;
    private readonly IProductsRepository _productsRepository;
    private readonly IProductVariationRepository _variationsRepository;
    private readonly IServicesRepository _servicesRepository;
    private readonly IOrderItemService _orderItemService;

    public UpdateOrderItemCommandHandler(
        IHttpContentResolver httpContentResolver,
        IOrderItemsRepository orderItemsRepository,
        IOrderItemModelsResolver orderItemModelsResolver,
        IOrderModelsResolver orderModelsResolver,
        IOrdersRepository ordersRepository,
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
        _orderModelsResolver = orderModelsResolver;
        _ordersRepository = ordersRepository;
        _productsRepository = productsRepository;
        _variationsRepository = variationsRepository;
        _servicesRepository = servicesRepository;
        _orderItemService = orderItemService;
    }

    public async Task<IActionResult> Handle(UpdateOrderItemCommand command, CancellationToken cancellationToken)
    {
        var orderItemRequest = await _httpContentResolver.GetPayloadAsync<UpdateOrderItemRequest>(command.Request);
        var originalDao = await _orderItemsRepository.GetAsync(Guid.Parse(command.Id));

        var product = orderItemRequest.ProductId is null ? null : await _productsRepository.GetAsync(Guid.Parse(orderItemRequest.ProductId));
        var productVariation = orderItemRequest.ProductVariationId is null
            ? null
            : await _variationsRepository.GetAsync(Guid.Parse(orderItemRequest.ProductVariationId));

        var service = orderItemRequest.ServiceId is null ? null : await _servicesRepository.GetAsync(Guid.Parse(orderItemRequest.ServiceId));

        var orderItemDao =
            _orderItemModelsResolver.GetDaoFromDaoAndRequest(originalDao, orderItemRequest, product, productVariation, service);


        using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _orderItemService.ResetQuantity(originalDao);
            await _orderItemsRepository.UpdateAsync(orderItemDao);
            await _orderItemService.SetQuantity(orderItemDao);

            transactionScope.Complete();
        }

        var orderDao = await _ordersRepository.GetAsync(orderItemDao.OrderId);
        var response = _orderModelsResolver.GetResponseFromDao(orderDao);

        return new OkObjectResult(response);
    }
}
