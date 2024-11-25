using Looms.PoS.Domain.Enums;
using System.Text.Json.Serialization;

namespace Looms.PoS.Application.Models.Responses;

public class DiscountResponse
{
    public Guid Id { get; init; }
    public string? Name { get; init; } = string.Empty;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DiscountType DiscountType { get; init; } 
    public decimal Value { get; init; } = 0;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DiscountTarget DiscountTarget { get; init; } 
    public Guid? ProductId { get; init; }
    public string StartDate { get; init; }
    public string EndDate { get; init; }
    public bool IsDeleted { get; init; } = false;
}
