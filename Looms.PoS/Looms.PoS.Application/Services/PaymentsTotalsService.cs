using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Services;

public class PaymentsTotalsService : IPaymentTotalsService
{
    public decimal CalculatePaymentTotal(IEnumerable<PaymentDao> payments)
    {
        var total = 0m;

        if (!payments.Any())
        {
            return 0;
        }

        foreach (var payment in payments)
        {
            total += payment.Amount;

            if (payment.Tip is not default(decimal))
            {
                total += payment.Tip;
            }
        }

        return total;
    }
}
