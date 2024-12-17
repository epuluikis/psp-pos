using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.Services;

public interface IOrderItemService
{
    decimal CalculateOrderItemPrice(OrderItemDao orderItemDao, DiscountDao? discountDao, int quantity);
    decimal CalculateOrderItemPrice(ProductDao? productDao, ProductVariationDao? productVariationDao, ServiceDao? serviceDao, int quantity);
    decimal CalculateOrderItemTax(TaxDao tax, decimal price);
    Task SetQuantity(OrderItemDao orderItemDao);
    Task ResetQuantity(OrderItemDao orderItemDao);
}
