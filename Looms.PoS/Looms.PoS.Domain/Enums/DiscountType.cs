namespace Looms.PoS.Domain.Enums;
using System.Runtime.Serialization;

public enum DiscountType
{
    [EnumMember(Value = "Percentage")]
    Percentage, 
    [EnumMember(Value = "Amount")]
    Amount 
}