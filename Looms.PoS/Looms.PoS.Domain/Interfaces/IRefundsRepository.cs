using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;
public interface IRefundsRepository
{
    Task<RefundDao> CreateAsync(RefundDao refundDao);
    IEnumerable<RefundDao> GetAll();
    Task<RefundDao> GetAsync(string id);
    void DeleteAsync(string id);
}
