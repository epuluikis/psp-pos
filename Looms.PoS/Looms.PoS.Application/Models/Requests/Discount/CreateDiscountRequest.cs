namespace Looms.PoS.Application.Models.Requests;

public record CreateDiscountRequest
{
    public string? Name { get; init; } = string.Empty;
    public string DiscountType { get; init; } = string.Empty;
    public decimal Value { get; init; } = 0;
    public string DiscountTarget { get; init; } = string.Empty;
    public string? ProductId { get; init; } = string.Empty;
    public string StartDate { get; init; } = string.Empty;
    public string EndDate { get; init; } = string.Empty;
}
