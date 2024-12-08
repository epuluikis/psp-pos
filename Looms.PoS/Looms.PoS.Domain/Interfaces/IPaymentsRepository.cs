using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IPaymentsRepository
{
    Task<PaymentDao> CreateAsync(PaymentDao paymentDao);
    Task<IEnumerable<PaymentDao>> GetAllAsync();
    Task<PaymentDao> GetAsync(Guid id);
    Task<PaymentDao> UpdateAsync(PaymentDao paymentDao);
}
