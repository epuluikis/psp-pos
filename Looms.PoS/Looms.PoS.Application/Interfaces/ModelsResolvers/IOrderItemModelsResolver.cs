using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Requests.OrderItem;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Application.Models.Responses.OrderItem;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;

public interface IOrderItemModelsResolver
{
    OrderItemDao GetDaoFromRequest(
        CreateOrderItemRequest createOrderItemRequest,
        Guid orderId,
        ProductDao? productDao,
        ProductVariationDao? productVariationDao,
        ServiceDao? serviceDao
    );

    OrderItemDao GetDaoFromDaoAndRequest(
        OrderItemDao orderItemDao,
        UpdateOrderItemRequest updateOrderItemRequest,
        ProductDao? productDao,
        ProductVariationDao? productVariationDao,
        ServiceDao? serviceDao
    );

    OrderItemResponse GetResponseFromDao(OrderItemDao orderItemDao);
    IEnumerable<OrderItemResponse> GetResponseFromDao(IEnumerable<OrderItemDao> orderItemDao);
    OrderItemDao GetDeletedDao(OrderItemDao originalDao);
}
