using System.Text.Json.Serialization;

namespace Looms.PoS.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PaymentStatus
{
    Pending,
    Succeeded,
    Failed
}
