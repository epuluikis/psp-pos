using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IRefundsRepository
{
    Task<RefundDao> CreateAsync(RefundDao refundDao);
    Task<IEnumerable<RefundDao>> GetAllAsync();
    Task<RefundDao> GetAsync(Guid id);
}
