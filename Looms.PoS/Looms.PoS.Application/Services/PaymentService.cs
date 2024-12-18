using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;

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

    private static decimal CalculateTotal(IEnumerable<PaymentDao> payments, bool includeAmount, bool includeTips)
    {
        var total = 0m;

        foreach (var payment in payments)
        {
            if (payment.Status is not PaymentStatus.Succeeded)
            {
                continue;
            }

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
