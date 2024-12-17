using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Services;

public class RefundService : IRefundService
{
    public decimal CalculateTotal(IEnumerable<RefundDao> refunds)
    {
        var total = 0m;

        if (!refunds.Any())
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
