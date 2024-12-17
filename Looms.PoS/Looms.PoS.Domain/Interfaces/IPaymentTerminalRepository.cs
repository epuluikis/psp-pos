using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IPaymentTerminalsRepository
{
    Task<PaymentTerminalDao> CreateAsync(PaymentTerminalDao paymentTerminalDao);
    Task<IEnumerable<PaymentTerminalDao>> GetAllAsync();
    Task<PaymentTerminalDao> GetAsync(Guid id);
    Task<PaymentTerminalDao> UpdateAsync(PaymentTerminalDao paymentTerminalDao);
    Task<PaymentTerminalDao> GetAsyncByIdAndBusinessId(Guid id, Guid businessId);
    Task<IEnumerable<PaymentTerminalDao>> GetAllAsyncByBusinessId(Guid businessId);
    Task<bool> ExistsWithExternalIdAndBusinessId(string externalId, Guid businessId);
    Task<bool> ExistsWithExternalIdAndBusinessIdExcludingId(string externalId, Guid businessId, Guid excludeId);
}
