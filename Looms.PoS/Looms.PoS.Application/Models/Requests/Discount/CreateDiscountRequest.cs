using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Models.Requests.Discount;

public record CreateDiscountRequest
{
    public string? Name { get; init; } = string.Empty;
    public DiscountType DiscountType { get; init; }
    public decimal Value { get; init; } = 0;
    public DiscountTarget Target { get; init; }
    public string? ProductId { get; init; }
    public string StartDate { get; init; } = string.Empty;
    public string EndDate { get; init; } = string.Empty;
}
