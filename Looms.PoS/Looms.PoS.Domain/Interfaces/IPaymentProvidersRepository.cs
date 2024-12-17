using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IPaymentProvidersRepository
{
    Task<PaymentProviderDao> CreateAsync(PaymentProviderDao paymentProviderDao);
    Task<IEnumerable<PaymentProviderDao>> GetAllAsync();
    Task<IEnumerable<PaymentProviderDao>> GetAllAsyncByBusinessId(Guid businessId);
    Task<PaymentProviderDao> GetAsync(Guid id);
    Task<PaymentProviderDao> GetAsyncByIdAndBusinessId(Guid id, Guid businessId);
    Task<PaymentProviderDao> UpdateAsync(PaymentProviderDao paymentProviderDao);
    Task<bool> ExistsActiveByBusinessId(Guid businessId);
    Task<bool> ExistsActiveByBusinessIdExcludingId(Guid excludeId, Guid businessId);
}
