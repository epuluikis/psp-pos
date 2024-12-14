using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IPaymentProvidersRepository
{
    Task<PaymentProviderDao> CreateAsync(PaymentProviderDao paymentProviderDao);
    Task<IEnumerable<PaymentProviderDao>> GetAllAsync();
    Task<PaymentProviderDao> GetAsync(Guid id);
    Task<PaymentProviderDao> UpdateAsync(PaymentProviderDao paymentProviderDao);
    Task<bool> ExistsActiveByBusinessId(Guid businessId);
    Task<bool> ExistsActiveByBusinessIdExcludingId(Guid excludeId, Guid businessId);
}
