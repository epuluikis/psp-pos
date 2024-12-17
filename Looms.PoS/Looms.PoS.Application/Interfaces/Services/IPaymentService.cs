using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.Services;

public interface IPaymentService
{
    decimal CalculateTotalWithTips(IEnumerable<PaymentDao> payments);
    decimal CalculateTotalWithoutTips(IEnumerable<PaymentDao> payments);
    decimal CalculateTips(IEnumerable<PaymentDao> payments);
}
