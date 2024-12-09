using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Domain.Interfaces;

public interface IOrdersRepository
{
    Task<OrderDao> CreateAsync(OrderDao order);
    Task<OrderDao> UpdateAsync(OrderDao order);
    Task<IEnumerable<OrderDao>> GetAllAsync();
    Task<OrderDao> GetAsync(Guid id);
    Task RemoveAsync(Guid id);
}