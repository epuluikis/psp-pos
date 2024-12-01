namespace Looms.PoS.Domain.Enums;
using System.Runtime.Serialization;

public enum DiscountTarget
{
    [EnumMember(Value = "Order")]
    Order, 
    [EnumMember(Value = "Product")]
    Product
}