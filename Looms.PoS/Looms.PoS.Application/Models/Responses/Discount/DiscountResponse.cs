using Looms.PoS.Domain.Enums;
using System.Text.Json.Serialization;

namespace Looms.PoS.Application.Models.Responses;

public record DiscountResponse
{
    public Guid Id { get; init; }
    public string? Name { get; init; } = string.Empty;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DiscountType DiscountType { get; init; } 
    public decimal Value { get; init; } = 0;
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DiscountTarget DiscountTarget { get; init; } 
    public Guid? ProductId { get; init; }
    public string StartDate { get; init; } = string.Empty;
    public string EndDate { get; init; } = string.Empty;
    public bool IsDeleted { get; init; } = false;
}
