using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using System.Net.WebSockets;

namespace Looms.PoS.Application.Utilities.Helpers;

// TODO: Uncomment when services have been added

public static class TotalsHelper
{
    public static decimal CalculateOrderItemPrice(ProductDao? productDao, ProductVariationDao? productVariationDao, int quantity)
    {
        decimal price = 0;

        if(productDao != null)
        {
            price = productDao.Price.Value * quantity;
        }
        else if(productVariationDao != null)
        {
            price = productVariationDao.Price.Value * quantity;
        }
        // else if (serviceDao != null)
        // {
        //     price = serviceDao.Price.Value * quantity;
        // }

        return price;
    }

    public static decimal CalculateOrderItemPrice(OrderItemDao orderItemDao, DiscountDao? discountDao, int quantity)
    {
        var price = orderItemDao.Price;
        if(orderItemDao.Quantity != quantity)
        {
            price = CalculateOrderItemPrice(orderItemDao.Product, orderItemDao.ProductVariation, quantity);
        }

        if(discountDao != null)
        {
            price = CalculateTotalWithDiscount(discountDao, price);
        }
        return price;
    }

    public static decimal CalculateOrderItemTax(TaxDao tax, decimal price)
    {
        return Math.Round(price * Decimal.Divide(tax.Percentage, 100), 2, MidpointRounding.AwayFromZero);
    }


// This will calculation could cause minor discrepancies in the total amount due to rounding errors

    public static decimal CalculateOrderTotal(OrderDao order)
    {
        decimal total = 0;
        decimal tax = 0;
        decimal price = 0;

        if(order.OrderItems.Count == 0)
        {
            return total;
        }

        foreach (var orderItem in order.OrderItems)
        {
            price += orderItem.Price;
            tax += orderItem.Tax;
        }   

        if(order.Discount != null)
        {
            if(order.Discount.DiscountType == DiscountType.Percentage)
            {
                foreach (var orderItem in order.OrderItems)
                {
                    var newItemPrice = CalculateTotalWithDiscount(order.Discount, orderItem.Price);
                    total += newItemPrice + CalculateOrderItemTax(orderItem.Product.Tax, newItemPrice);
                }
            }
            else
            {
                total = price - order.Discount.Value;
                tax = 0;

                if(total < 0)
                {
                    return 0;
                }

                // calculates new price for each item in the order and based off of that calculates the tax
                foreach (var orderItem in order.OrderItems)
                {
                    var newItemPrice = Math.Round(total * orderItem.Price / price, 2, MidpointRounding.AwayFromZero);
                    tax += CalculateOrderItemTax(orderItem.Product.Tax, newItemPrice);
                }
                total += tax;
            }

        }
        else
        {
            total = price + tax;
        }

        return total;
    }

    public static decimal CalculateOrderTax(OrderDao order)
    {
        decimal tax = 0;
        decimal price = 0;
        decimal taxTotal = 0;

        if(order.OrderItems.Count == 0)
        {
            return taxTotal;
        }

        foreach (var orderItem in order.OrderItems)
        {
            price += orderItem.Price;
            tax += orderItem.Tax;
        }  

        if(order.Discount != null)
        {
            if(order.Discount.DiscountType == DiscountType.Percentage)
            {
                foreach (var orderItem in order.OrderItems)
                {
                    var newItemPrice = CalculateTotalWithDiscount(order.Discount, orderItem.Price);
                    taxTotal += CalculateOrderItemTax(orderItem.Product.Tax, newItemPrice);
                }
            }
            else
            {
                var total = price - order.Discount.Value;
                tax = 0;

                if(total < 0)
                {
                    return 0;
                }

                // calculates new price for each item in the order and based off of that calculates the tax
                foreach (var orderItem in order.OrderItems)
                {
                    var newItemPrice = Math.Round(total * orderItem.Price / price, 2, MidpointRounding.AwayFromZero);
                    tax += CalculateOrderItemTax(orderItem.Product.Tax, newItemPrice);
                }
                taxTotal += tax;
            }
        }
        else
        {
            taxTotal = tax;
        }

        return taxTotal;
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
            total = Math.Round(total * Decimal.Divide((100 - discount.Value), 100), 2, MidpointRounding.AwayFromZero);
        }
        else
        {
            if(total < discount.Value)
            {
                return 0;
            }

            total -= discount.Value;
        }

        return total;
    }
}
