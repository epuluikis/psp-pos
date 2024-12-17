using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Services;

public class OrderItemService : IOrderItemService
{
    private readonly IDiscountService _discountService;
    private readonly IProductService _productService;
    private readonly IProductVariationService _productVariationService;

    public OrderItemService(
        IDiscountService discountService,
        IProductService productService,
        IProductVariationService productVariationService
    )
    {
        _discountService = discountService;
        _productService = productService;
        _productVariationService = productVariationService;
    }

    public decimal CalculateOrderItemPrice(OrderItemDao orderItemDao, DiscountDao? discountDao, int quantity)
    {
        var price = orderItemDao.Price;

        if (orderItemDao.Quantity != quantity)
        {
            price = CalculateOrderItemPrice(orderItemDao.Product, orderItemDao.ProductVariation, orderItemDao.Service, quantity);
        }

        if (discountDao is not null)
        {
            price = _discountService.CalculateTotalWithDiscount(discountDao, price);
        }

        return price;
    }

    public decimal CalculateOrderItemPrice(
        ProductDao? productDao,
        ProductVariationDao? productVariationDao,
        ServiceDao? serviceDao,
        int quantity)
    {
        var price = 0m;

        if (serviceDao is not null)
        {
            return serviceDao.Price * quantity;
        }

        if (productDao is not null)
        {
            price = productDao.Price.Value * quantity;
        }

        if (productVariationDao is not null)
        {
            price = productVariationDao.Price.Value * quantity;
        }

        return price;
    }

    public decimal CalculateOrderItemTax(TaxDao tax, decimal price)
    {
        return Math.Round(price * decimal.Divide(tax.Percentage, 100), 2, MidpointRounding.AwayFromZero);
    }

    public async Task SetQuantity(OrderItemDao orderItemDao)
    {
        if (orderItemDao.Product is not null)
        {
            await _productService.UpdateStock(orderItemDao.Product, orderItemDao.Quantity);
        }

        if (orderItemDao.ProductVariation is not null)
        {
            await _productVariationService.UpdateStock(orderItemDao.ProductVariation, orderItemDao.Quantity);
        }
    }

    public async Task ResetQuantity(OrderItemDao orderItemDao)
    {
        if (orderItemDao.Product is not null)
        {
            await _productService.UpdateStock(orderItemDao.Product, -orderItemDao.Quantity);
        }

        if (orderItemDao.ProductVariation is not null)
        {
            await _productVariationService.UpdateStock(orderItemDao.ProductVariation, -orderItemDao.Quantity);
        }
    }
}
