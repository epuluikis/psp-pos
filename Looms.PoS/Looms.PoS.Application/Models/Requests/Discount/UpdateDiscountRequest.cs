using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Models.Requests.Discount;

public record UpdateDiscountRequest
{
    public string? Name { get; init; } = string.Empty;
    public DiscountType DiscountType { get; init; }
    public decimal Value { get; init; }
    public DiscountTarget Target { get; init; }
    public string? ProductId { get; init; } = string.Empty;
    public string StartDate { get; init; } = string.Empty;
    public string EndDate { get; init; } = string.Empty;
}
