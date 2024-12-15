using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.Services;

public interface IOrderItemTotalsService
{
    decimal CalculateOrderItemPrice(OrderItemDao orderItemDao, DiscountDao? discountDao, int quantity);
    decimal CalculateOrderItemPrice(ProductDao? productDao, ProductVariationDao? productVariationDao, int quantity);
    decimal CalculateOrderItemTax(TaxDao tax, decimal price);
}