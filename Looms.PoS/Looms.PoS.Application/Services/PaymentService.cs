using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Services;

public class PaymentService : IPaymentService
{
    public decimal CalculateTotalWithTips(IEnumerable<PaymentDao> payments)
    {
        return CalculateTotal(payments, true, true);
    }

    public decimal CalculateTotalWithoutTips(IEnumerable<PaymentDao> payments)
    {
        return CalculateTotal(payments, true, false);
    }

    public decimal CalculateTips(IEnumerable<PaymentDao> payments)
    {
        return CalculateTotal(payments, false, true);
    }

    private decimal CalculateTotal(IEnumerable<PaymentDao> payments, bool includeAmount, bool includeTips)
    {
        var total = 0m;

        foreach (var payment in payments)
        {
            if (includeAmount)
            {
                total += payment.Amount;
            }

            if (includeTips)
            {
                total += payment.Tip;
            }
        }

        return total;
    }
}
