using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IOrderItemsRepository
{
    Task<OrderItemDao> CreateAsync(OrderItemDao orderItem);
    Task<OrderItemDao> UpdateVariationAsync(Guid productVariationId);
    Task<OrderItemDao> UpdateQuantityAsync(int quantity);
    Task<OrderItemDao> UpdateDiscountAsync(Guid discoundId);
    Task<IEnumerable<OrderItemDao>> GetAllAsync();
    Task<OrderItemDao> GetAsync(Guid id);
    Task Save();
    Task DeleteAsync(Guid id);
}