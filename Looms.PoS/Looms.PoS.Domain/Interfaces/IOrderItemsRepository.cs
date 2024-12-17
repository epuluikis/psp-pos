using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IOrderItemsRepository
{
    Task<OrderItemDao> CreateAsync(OrderItemDao orderItem);
    Task<OrderItemDao> UpdateAsync(OrderItemDao orderItem);
    Task<IEnumerable<OrderItemDao>> GetAllAsync(Guid orderId);
    Task<OrderItemDao> GetAsync(Guid id);
    Task<OrderItemDao> GetAsyncByIdAndOrderIdAndBusinessId(Guid id, Guid orderId, Guid businessId);
}
