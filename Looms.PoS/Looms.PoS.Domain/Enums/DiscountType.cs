using System.Text.Json.Serialization;

namespace Looms.PoS.Domain.Enums;
using System.Runtime.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DiscountType
{
    Percentage,
    Amount
}
