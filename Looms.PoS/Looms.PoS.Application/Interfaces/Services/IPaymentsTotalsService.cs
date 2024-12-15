using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.Services;

public interface IPaymentTotalsService
{
    decimal CalculatePaymentTotal(IEnumerable<PaymentDao> payments);
}