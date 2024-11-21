using System.Runtime.Serialization;

namespace Looms.PoS.Domain.Enums;

public enum OrderStatus
{
    [EnumMember(Value = "Pending")]
    Pending,
    [EnumMember(Value = "Paid")]
    Paid,
    [EnumMember(Value = "Cancelled")]
    Cancelled,
    [EnumMember(Value = "Refunded")]
    Refunded
}