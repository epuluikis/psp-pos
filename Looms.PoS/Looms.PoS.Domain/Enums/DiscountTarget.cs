namespace Looms.PoS.Domain.Enums;

public enum DiscountTarget
{
    [EnumMember(Value = "Order")]
    Order, 
    [EnumMember(Value = "Product")]
    Product
}