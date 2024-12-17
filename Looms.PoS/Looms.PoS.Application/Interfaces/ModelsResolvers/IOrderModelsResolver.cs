using Looms.PoS.Application.Features.Order.Queries.GetOrders;
using Looms.PoS.Application.Models.Requests.Order;
using Looms.PoS.Application.Models.Responses.Order;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Filters.Order;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;

public interface IOrderModelsResolver
{
    GetAllOrdersFilter GetFiltersFromQuery(GetOrdersQuery getOrdersQuery);
    OrderDao GetDaoFromRequest(CreateOrderRequest createOrderRequest, BusinessDao businessDao, UserDao userDao);
    OrderDao GetDaoFromDaoAndRequest(OrderDao orderDao, UpdateOrderRequest updateOrderRequest, DiscountDao? discountDao);
    OrderResponse GetResponseFromDao(OrderDao orderDao);
    IEnumerable<OrderResponse> GetResponseFromDao(IEnumerable<OrderDao> orderDao);
    OrderDao GetDeletedDao(OrderDao originalDao);
}
