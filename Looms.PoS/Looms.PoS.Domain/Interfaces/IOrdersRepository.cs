using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Domain.Interfaces;

public interface IOrdersRepository
{
    Task<OrderDao> CreateAsync(OrderDao order);
    Task<OrderDao> UpdateStatusAsync(Guid orderId, OrderStatus status);
    Task<OrderDao> UpdateDiscountAsync(Guid orderId, Guid discountId);
    Task<OrderDao> UpdateUserAsync(Guid orderId, Guid userId);
    Task<OrderDao> UpdateItemsAsync(Guid orderId, ICollection<OrderItemDao> orderItems);
    Task<OrderDao> UpdateItemAsync(Guid orderId, OrderItemDao orderItem);
    Task<IEnumerable<OrderDao>> GetAllAsync();
    Task<OrderDao> GetAsync(Guid id);
    Task Save();
    Task RemoveAsync(Guid id);
}