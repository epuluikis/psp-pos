using AutoMapper;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Requests.OrderItem;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Application.Models.Responses.OrderItem;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.ModelsResolvers;

public class OrderItemModelsResolver : IOrderItemModelsResolver
{
    private readonly IMapper _mapper;
    private readonly IOrderItemService _orderItemService;

    public OrderItemModelsResolver(
        IMapper mapper,
        IOrderItemService orderItemService
    )
    {
        _mapper = mapper;
        _orderItemService = orderItemService;
    }

    public OrderItemDao GetDaoFromRequest(
        CreateOrderItemRequest createOrderItemRequest,
        Guid orderId,
        ProductDao? productDao,
        ProductVariationDao? productVariationDao,
        ServiceDao? serviceDao)
    {
        var price = _orderItemService.CalculateOrderItemPrice(
            productDao,
            productVariationDao,
            serviceDao,
            createOrderItemRequest.Quantity
        );

        decimal tax = 0;

        var taxDao = productDao?.Tax ?? serviceDao?.Tax;

        if (taxDao != null)
        {
            tax = _orderItemService.CalculateOrderItemTax(taxDao, price);
        }

        return _mapper.Map<OrderItemDao>(createOrderItemRequest) with
        {
            OrderId = orderId,
            Product = productDao,
            ProductId = productDao?.Id,
            ProductVariation = productVariationDao,
            ProductVariationId = productVariationDao?.Id,
            Service = serviceDao,
            ServiceId = serviceDao?.Id,
            Price = price,
            Tax = tax
        };
    }

    public OrderItemDao GetDaoFromDaoAndRequest(
        OrderItemDao orderItemDao,
        UpdateOrderItemRequest updateOrderItemRequest,
        ProductDao? productDao,
        ProductVariationDao? productVariationDao,
        ServiceDao? serviceDao)
    {
        var price = _orderItemService.CalculateOrderItemPrice(
            productDao,
            productVariationDao,
            serviceDao,
            updateOrderItemRequest.Quantity
        );

        var tax = 0m;

        var taxDao = productDao?.Tax ?? serviceDao?.Tax;

        if (taxDao != null)
        {
            tax = _orderItemService.CalculateOrderItemTax(taxDao, price);
        }

        return _mapper.Map<OrderItemDao>(updateOrderItemRequest) with
        {
            OrderId = orderItemDao.Id,
            Product = productDao,
            ProductId = productDao?.Id,
            ProductVariation = productVariationDao,
            ProductVariationId = productVariationDao?.Id,
            Service = serviceDao,
            ServiceId = serviceDao?.Id,
            Price = price,
            Tax = tax
        };
    }

    public OrderItemResponse GetResponseFromDao(OrderItemDao orderItemDao)
    {
        return _mapper.Map<OrderItemResponse>(orderItemDao);
    }


    public IEnumerable<OrderItemResponse> GetResponseFromDao(IEnumerable<OrderItemDao> orderItemDao)
    {
        return _mapper.Map<IEnumerable<OrderItemResponse>>(orderItemDao);
    }

    public OrderItemDao GetDeletedDao(OrderItemDao originalDao)
    {
        return originalDao with { Service = null, ServiceId = null, IsDeleted = true };
    }
}
