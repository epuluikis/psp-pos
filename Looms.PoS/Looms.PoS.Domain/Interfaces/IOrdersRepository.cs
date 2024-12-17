using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Filters.Order;

namespace Looms.PoS.Domain.Interfaces;

public interface IOrdersRepository
{
    Task<OrderDao> CreateAsync(OrderDao order);
    Task<OrderDao> UpdateAsync(OrderDao order);
    Task<IEnumerable<OrderDao>> GetAllAsync();
    Task<IEnumerable<OrderDao>> GetAllAsync(GetAllOrdersFilter filter);
    Task<OrderDao> GetAsync(Guid id);
}
