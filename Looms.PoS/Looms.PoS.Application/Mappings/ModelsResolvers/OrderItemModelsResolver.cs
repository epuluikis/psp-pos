using AutoMapper;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
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
        return _mapper.Map(updateOrderItemRequest, orderItemDao) with
        {
            Id = orderItemDao.Id,
            Discount = discountDao
        };    
    }

    public OrderItemDao GetDaoFromRequest(CreateOrderItemRequest createOrderItemRequest)
    => _mapper.Map<OrderItemDao>(createOrderItemRequest);

    public OrderItemResponse GetResponseFromDao(OrderItemDao orderItemDao)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<OrderItemResponse> GetResponseFromDao(IEnumerable<OrderItemDao> orderItemDao)
    {
        throw new NotImplementedException();
    }

    public OrderItemDao GetDeletedDao(OrderItemDao originalDao)
    {
        return originalDao with
        {
            IsDeleted = true
        };
    }
}