using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Services;

public class OrderTotalsService : IOrderTotalsService
{
    private readonly IOrderItemTotalsService _orderItemTotalsService;
    private readonly IDiscountTotalsService _discountTotalsService;

    public OrderTotalsService(IOrderItemTotalsService orderItemTotalsService, IDiscountTotalsService discountTotalsService)
    {
        _orderItemTotalsService = orderItemTotalsService;
        _discountTotalsService = discountTotalsService;
    }

    public decimal CalculateOrderTotal(OrderDao order)
    {
        decimal total = 0;

        if(order.OrderItems.Count == 0)
        {
            return total;
        }

        var price = order.OrderItems.Sum(orderItem => orderItem.Price);
        var tax = order.OrderItems.Sum(orderItem => orderItem.Tax);

        if(order.Discount is not null)
        {
            if(order.Discount.DiscountType is DiscountType.Percentage)
            {
                total = CalculateTotalWithPercentageDiscount(order.OrderItems, order.Discount);
            }else
            {
                total = CalculateTotalWithAmountDiscount(order.OrderItems, price - order.Discount.Value, price);
            }
        }else
        {
            total = price + tax;
        }

        return total;
    }

    public decimal CalculateOrderTax(OrderDao order)
    {
        decimal totalTax = 0;

        if(order.OrderItems.Count == 0)
        {
            return totalTax;
        }

        var price = order.OrderItems.Sum(orderItem => orderItem.Price);
        var tax = order.OrderItems.Sum(orderItem => orderItem.Tax);

        if(order.Discount is not null)
        {
            if(order.Discount.DiscountType is DiscountType.Percentage)
            {
                totalTax = CalculateTaxWithPercentageDiscount(order.OrderItems, order.Discount);
            }else
            {
                totalTax = CalculateTaxWithAmountDiscount(order.OrderItems, price - order.Discount.Value, price);
            }
        }else
        {
            totalTax = tax;
        }
    
        return totalTax;
    }

    private decimal CalculateTotalWithPercentageDiscount(IEnumerable<OrderItemDao> orderItems, DiscountDao discount)
    {
        decimal total = 0;
        
        foreach (var orderItem in orderItems)
        {
            var newItemPrice = _discountTotalsService.CalculateTotalWithDiscount(discount, orderItem.Price);
            total += newItemPrice + _orderItemTotalsService.CalculateOrderItemTax(orderItem.Product!.Tax, newItemPrice);
        }
        return total;
    }

    private decimal CalculateTotalWithAmountDiscount(IEnumerable<OrderItemDao> orderItems, decimal discountedPrice, decimal originalPrice)
    {
        decimal tax = 0;
        
        if(discountedPrice < 0)
        {
            return 0;
        }

        foreach (var orderItem in orderItems)
        {
            var newItemPrice = Math.Round(discountedPrice * orderItem.Price / originalPrice, 2, MidpointRounding.AwayFromZero);
            tax += _orderItemTotalsService.CalculateOrderItemTax(orderItem.Product!.Tax, newItemPrice);
        }
        var total = tax + discountedPrice;

        return total;
    }

    private decimal CalculateTaxWithPercentageDiscount(IEnumerable<OrderItemDao> orderItems, DiscountDao discount)
    {
        decimal taxTotal = 0;
        
        foreach (var orderItem in orderItems)
        {
            var newItemPrice = _discountTotalsService.CalculateTotalWithDiscount(discount, orderItem.Price);
            taxTotal += _orderItemTotalsService.CalculateOrderItemTax(orderItem.Product!.Tax, newItemPrice);
        }
        return taxTotal;
    }

    private decimal CalculateTaxWithAmountDiscount(IEnumerable<OrderItemDao> orderItems, decimal discountedPrice, decimal originalPrice)
    {
        decimal tax = 0;

        if(discountedPrice < 0)
        {
            return 0;
        }

        foreach (var orderItem in orderItems)
        {
            var newItemPrice = Math.Round(discountedPrice * orderItem.Price / originalPrice, 2, MidpointRounding.AwayFromZero);
            tax += _orderItemTotalsService.CalculateOrderItemTax(orderItem.Product!.Tax, newItemPrice);
        }

        return tax;
    }

}