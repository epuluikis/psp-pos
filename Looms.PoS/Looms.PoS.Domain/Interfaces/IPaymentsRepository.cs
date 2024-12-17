using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IPaymentsRepository
{
    Task<PaymentDao> CreateAsync(PaymentDao paymentDao);
    Task<IEnumerable<PaymentDao>> GetAllAsync();
    Task<IEnumerable<PaymentDao>> GetAllAsyncByBusinessId(Guid businessId);
    Task<PaymentDao> GetAsync(Guid id);
    Task<PaymentDao> GetAsyncByIdAndBusinessId(Guid id, Guid businessId);
    Task<PaymentDao> GetAsyncByExternalId(string externalId);
    Task<PaymentDao> UpdateAsync(PaymentDao paymentDao);
}
