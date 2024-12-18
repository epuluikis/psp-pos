using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Services;

public class RefundService : IRefundService
{
    public decimal CalculateTotal(IEnumerable<RefundDao> refunds)
    {
        return refunds.Where(refund => refund.Status is RefundStatus.Completed).Sum(refund => refund.Amount);
    }

    public decimal CalculateRefundableAmountForPayment(PaymentDao payment)
    {
        return payment.Amount
             + payment.Tip
             - payment.Refunds.Where(refund => refund.Status is RefundStatus.Completed or RefundStatus.Pending)
                      .Sum(refund => refund.Amount);
    }
}
