using System.Runtime.Serialization;

namespace Looms.PoS.Domain.Enums;

public enum DiscountType
{
    [EnumMember(Value = "Percentage")]
    Percentage, 
    [EnumMember(Value = "Amount")]
    Amount 
}