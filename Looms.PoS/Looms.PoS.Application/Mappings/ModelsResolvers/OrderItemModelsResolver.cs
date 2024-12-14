using AutoMapper;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Application.Utilities.Helpers;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.ModelsResolvers;

public class OrderItemModelsResolver : IOrderItemModelsResolver
{
    private readonly IMapper _mapper;

    public OrderItemModelsResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public OrderItemDao GetDaoFromDaoAndRequest(OrderItemDao orderItemDao, UpdateOrderItemRequest updateOrderItemRequest, DiscountDao? discountDao)
    {
        var price = TotalsHelper.CalculateOrderItemPrice(orderItemDao, discountDao, orderItemDao.Quantity);
        var tax = TotalsHelper.CalculateOrderItemTax(orderItemDao.Product?.Tax, price);

        return _mapper.Map(updateOrderItemRequest, orderItemDao) with
        {
            Id = orderItemDao.Id,
            Discount = discountDao,
            Price = price,
            Tax = tax
        };    
    }

    public OrderItemDao GetDaoFromRequest(Guid orderId, CreateOrderItemRequest createOrderItemRequest, ProductDao? productDao, ProductVariationDao? productVariationDao)
    {
        var price = TotalsHelper.CalculateOrderItemPrice(productDao, productVariationDao, createOrderItemRequest.Quantity);
        var tax = TotalsHelper.CalculateOrderItemTax(productDao?.Tax, price);

        return _mapper.Map<OrderItemDao>(createOrderItemRequest) with
        {
            OrderId = orderId,
            Product = productDao,
            ProductId = productDao?.Id,
            ProductVariation = productVariationDao,
            ProductVariationId = productVariationDao?.Id,
            Price = price,
            Tax = tax
        };    
    }

    public OrderItemResponse GetResponseFromDao(OrderItemDao orderItemDao)
        => _mapper.Map<OrderItemResponse>(orderItemDao);


    public IEnumerable<OrderItemResponse> GetResponseFromDao(IEnumerable<OrderItemDao> orderItemDao)
        => _mapper.Map<IEnumerable<OrderItemResponse>>(orderItemDao);

    public OrderItemDao GetDeletedDao(OrderItemDao originalDao)
    {
        return originalDao with
        {
            IsDeleted = true
        };
    }
}