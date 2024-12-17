using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderItemService _orderItemService;
    private readonly IDiscountService _discountService;

    public OrderService(IOrderItemService orderItemService, IDiscountService discountService)
    {
        _orderItemService = orderItemService;
        _discountService = discountService;
    }

    public decimal CalculateTotal(OrderDao order)
    {
        decimal total = 0;

        if (order.OrderItems.Count == 0)
        {
            return total;
        }

        var price = order.OrderItems.Sum(orderItem => orderItem.Price);
        var tax = order.OrderItems.Sum(orderItem => orderItem.Tax);

        if (order.Discount is not null)
        {
            if (order.Discount.DiscountType is DiscountType.Percentage)
            {
                total = CalculateTotalWithPercentageDiscount(order.OrderItems, order.Discount);
            }
            else
            {
                total = CalculateTotalWithAmountDiscount(order.OrderItems, price - order.Discount.Value, price);
            }
        }
        else
        {
            total = price + tax;
        }

        return total;
    }

    public decimal CalculateTax(OrderDao order)
    {
        decimal totalTax = 0;

        if (order.OrderItems.Count == 0)
        {
            return totalTax;
        }

        var price = order.OrderItems.Sum(orderItem => orderItem.Price);
        var tax = order.OrderItems.Sum(orderItem => orderItem.Tax);

        if (order.Discount is not null)
        {
            if (order.Discount.DiscountType is DiscountType.Percentage)
            {
                totalTax = CalculateTaxWithPercentageDiscount(order.OrderItems, order.Discount);
            }
            else
            {
                totalTax = CalculateTaxWithAmountDiscount(order.OrderItems, price - order.Discount.Value, price);
            }
        }
        else
        {
            totalTax = tax;
        }

        return totalTax;
    }

    public async Task SetQuantity(OrderDao orderDao)
    {
        List<Task> tasks = [];

        foreach (var orderItem in orderDao.OrderItems)
        {
            tasks.Add(_orderItemService.SetQuantity(orderItem));
        }

        await Task.WhenAll(tasks);
    }

    public Task RecalculateQuantity(OrderDao newOrderDao, OrderDao oldOrderDao)
    {
        // TODO
        throw new NotImplementedException();
    }

    public async Task ResetQuantity(OrderDao orderDao)
    {
        List<Task> tasks = [];

        foreach (var orderItem in orderDao.OrderItems)
        {
            tasks.Add(_orderItemService.ResetQuantity(orderItem));
        }

        await Task.WhenAll(tasks);
    }

    private decimal CalculateTotalWithPercentageDiscount(IEnumerable<OrderItemDao> orderItems, DiscountDao discount)
    {
        decimal total = 0;

        foreach (var orderItem in orderItems)
        {
            var newItemPrice = _discountService.CalculateTotalWithDiscount(discount, orderItem.Price);
            total += newItemPrice + _orderItemService.CalculateOrderItemTax(orderItem.Product!.Tax, newItemPrice);
        }

        return total;
    }

    private decimal CalculateTotalWithAmountDiscount(IEnumerable<OrderItemDao> orderItems, decimal discountedPrice, decimal originalPrice)
    {
        decimal tax = 0;

        if (discountedPrice < 0)
        {
            return 0;
        }

        foreach (var orderItem in orderItems)
        {
            var newItemPrice = Math.Round(discountedPrice * orderItem.Price / originalPrice, 2, MidpointRounding.AwayFromZero);
            tax += _orderItemService.CalculateOrderItemTax(orderItem.Product!.Tax, newItemPrice);
        }

        var total = tax + discountedPrice;

        return total;
    }

    private decimal CalculateTaxWithPercentageDiscount(IEnumerable<OrderItemDao> orderItems, DiscountDao discount)
    {
        decimal taxTotal = 0;

        foreach (var orderItem in orderItems)
        {
            var newItemPrice = _discountService.CalculateTotalWithDiscount(discount, orderItem.Price);
            taxTotal += _orderItemService.CalculateOrderItemTax(orderItem.Product!.Tax, newItemPrice);
        }

        return taxTotal;
    }

    private decimal CalculateTaxWithAmountDiscount(IEnumerable<OrderItemDao> orderItems, decimal discountedPrice, decimal originalPrice)
    {
        decimal tax = 0;

        if (discountedPrice < 0)
        {
            return 0;
        }

        foreach (var orderItem in orderItems)
        {
            var newItemPrice = Math.Round(discountedPrice * orderItem.Price / originalPrice, 2, MidpointRounding.AwayFromZero);
            tax += _orderItemService.CalculateOrderItemTax(orderItem.Product!.Tax, newItemPrice);
        }

        return tax;
    }
}