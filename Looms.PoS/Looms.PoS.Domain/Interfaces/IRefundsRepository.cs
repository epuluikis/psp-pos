using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IRefundsRepository
{
    Task<RefundDao> CreateAsync(RefundDao refundDao);
    Task<IEnumerable<RefundDao>> GetAllAsync();
    Task<IEnumerable<RefundDao>> GetAllAsyncByBusinessId(Guid businessId);
    Task<RefundDao> GetAsync(Guid id);
    Task<RefundDao> GetAsyncByIdAndBusinessId(Guid id, Guid businessId);
    Task<RefundDao> GetAsyncByExternalId(string externalId);
    Task<RefundDao> UpdateAsync(RefundDao refundDao);
}
