using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Services;

public class OrderItemTotalsService : IOrderItemTotalsService
{
    private readonly IDiscountTotalsService _discountTotalsService;

    public OrderItemTotalsService(IDiscountTotalsService discountTotalsService)
    {
        _discountTotalsService = discountTotalsService;
    }

    public decimal CalculateOrderItemPrice(OrderItemDao orderItemDao, DiscountDao? discountDao, int quantity)
    {
        var price = orderItemDao.Price;
        if(orderItemDao.Quantity != quantity)
        {
            price = CalculateOrderItemPrice(orderItemDao.Product, orderItemDao.ProductVariation, orderItemDao.Service, quantity);
        }

        if(discountDao is not null)
        {
            price = _discountTotalsService.CalculateTotalWithDiscount(discountDao, price);
        }
        return price;
    }

    public decimal CalculateOrderItemPrice(ProductDao? productDao, ProductVariationDao? productVariationDao, ServiceDao? serviceDao, int quantity)
    {
        decimal price = 0;

        if(serviceDao is not null)
        {
            return serviceDao.Price * quantity;
        }

        if(productDao is not null)
        {
            price = productDao.Price.Value * quantity;
        }
        if(productVariationDao is not null)
        {
            price = productVariationDao.Price.Value * quantity;
        }

        return price;
    }

    public decimal CalculateOrderItemTax(TaxDao tax, decimal price)
    {
        return Math.Round(price * Decimal.Divide(tax.Percentage, 100), 2, MidpointRounding.AwayFromZero);
    }
}