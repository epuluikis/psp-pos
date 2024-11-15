using Looms.PoS.Domain.Enums;
using System.Text.Json.Serialization;

namespace Looms.PoS.Application.Models.Responses;

public class DiscountResponse
{
    public Guid Id { get; init; }
    public string? Name { get; set; } = string.Empty;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DiscountType DiscountType { get; set; }
    public decimal Value { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DiscountTarget DiscountTarget { get; set; }
    public Guid? ProductId { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public bool IsDeleted { get; set; } = false;
}
