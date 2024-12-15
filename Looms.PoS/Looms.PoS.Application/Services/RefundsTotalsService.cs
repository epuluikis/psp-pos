using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Services;

public class RefundsTotalsService : IRefundsTotalsService
{
    public decimal CalculateRefundTotal(IEnumerable<RefundDao> refunds)
    {
        decimal total = 0;
        if(!refunds.Any())
        {
            return 0;
        }

        foreach (var refund in refunds)
        {
            total += refund.Amount;
        }

        return total;
    }
}
