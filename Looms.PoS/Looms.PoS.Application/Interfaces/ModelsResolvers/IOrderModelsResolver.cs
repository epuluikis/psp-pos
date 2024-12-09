using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;

public interface IOrderModelsResolver
{
    OrderDao GetDaoFromRequest(CreateOrderRequest createOrderRequest, BusinessDao businessDao);
    OrderDao GetDaoFromDaoAndRequest(OrderDao orderDao, UpdateOrderRequest updateOrderRequest, DiscountDao? discountDao);
    OrderResponse GetResponseFromDao(OrderDao orderDao);
    IEnumerable<OrderResponse> GetResponseFromDao(IEnumerable<OrderDao> orderDao);
    OrderDao GetDeletedDao(OrderDao originalDao);
}
