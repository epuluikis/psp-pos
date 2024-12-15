using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IPaymentTerminalsRepository
{
    Task<PaymentTerminalDao> CreateAsync(PaymentTerminalDao paymentTerminalDao);
    Task<IEnumerable<PaymentTerminalDao>> GetAllAsync();
    Task<PaymentTerminalDao> GetAsync(Guid id);
    Task<PaymentTerminalDao> UpdateAsync(PaymentTerminalDao paymentTerminalDao);
}
