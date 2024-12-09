using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using System.Net.Http.Headers;

namespace Looms.PoS.Application.Utilities.Helpers;

public static class TotalsHelper
{
    public static decimal CalculateOrderItemTotal(OrderItemDao orderItem)
    {
        var total = orderItem.Price * orderItem.Quantity + orderItem.TaxAmount;

        if(orderItem.Discount != null)
        {
            total = CalculateTotalWithDiscount(orderItem.Discount, total);
        }

        return total;
    }

    public static decimal CalculateOrderTotal(OrderDao order)
    {
        decimal total = 0;
        if(order.OrderItems.Count == 0)
        {
            return total;
        }

        foreach (var orderItem in order.OrderItems)
        {
            total += CalculateOrderItemTotal(orderItem);
        }

        if(order.Discount != null)
        {
            total = CalculateTotalWithDiscount(order.Discount, total);
        }

        return total;
    }

    public static decimal CalculatePaymentTotal(IEnumerable<PaymentDao> payments)
    {
        decimal total = 0;
        if(!payments.Any())
        {
            return 0;
        }

        foreach (var payment in payments)
        {
            total += payment.Amount;
            if(payment.Tip != default(decimal))
            {
                total += payment.Tip;
            }
        }

        return total;
    }

    public static decimal CalculateRefundTotal(IEnumerable<RefundDao> refunds)
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

    private static decimal CalculateTotalWithDiscount(DiscountDao discount, decimal total)
    {
        if(discount.DiscountType == DiscountType.Percentage)
        {
            total -= total * (100 - discount.Value);
        }
        else
        {
            total -= discount.Value;
        }

        return total;
    }
}
