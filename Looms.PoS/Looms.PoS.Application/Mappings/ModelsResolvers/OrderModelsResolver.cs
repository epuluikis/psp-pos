using AutoMapper;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Mappings.ModelsResolvers;

public class OrderModelsResolver : IOrderModelsResolver
{
    private readonly IMapper _mapper;

// TODO: Add same thing as for business for user 

    public OrderModelsResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public OrderDao GetDaoFromRequest(CreateOrderRequest createOrderRequest, BusinessDao businessDao)
    {
        return _mapper.Map<OrderDao>(createOrderRequest) with 
        {
            Business = businessDao
        };
    }

    public OrderDao GetDaoFromDaoAndRequest(OrderDao orderDao, UpdateOrderRequest updateOrderRequest, DiscountDao? discountDao)
    {
        return _mapper.Map(updateOrderRequest, orderDao) with
        {
            Id = orderDao.Id,
            Discount = discountDao
        };
    }

    public OrderResponse GetResponseFromDao(OrderDao orderDao)
        => _mapper.Map<OrderResponse>(orderDao);

    public IEnumerable<OrderResponse> GetResponseFromDao(IEnumerable<OrderDao> orderDao)
        => _mapper.Map<IEnumerable<OrderResponse>>(orderDao);

    public OrderDao GetDeletedDao(OrderDao originalDao){
        return originalDao with
        {
            IsDeleted = true
        };
    }
}