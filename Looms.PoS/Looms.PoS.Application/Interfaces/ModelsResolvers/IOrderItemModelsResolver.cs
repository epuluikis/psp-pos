using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;

public interface IOrderItemModelsResolver
{
    OrderItemDao GetDaoFromRequest(CreateOrderItemRequest createOrderItemRequest);
    OrderItemDao GetDaoFromDaoAndRequest(OrderItemDao orderItemDao, UpdateOrderItemRequest updateOrderItemRequest, DiscountDao? discountDao);
    OrderItemResponse GetResponseFromDao(OrderItemDao orderItemDao);
    IEnumerable<OrderItemResponse> GetResponseFromDao(IEnumerable<OrderItemDao> orderItemDao);
    OrderItemDao GetDeletedDao(OrderItemDao originalDao);
}
