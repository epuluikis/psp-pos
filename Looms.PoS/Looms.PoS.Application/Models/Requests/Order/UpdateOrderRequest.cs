namespace Looms.PoS.Application.Models.Requests.Order;

public record UpdateOrderRequest
{
    public string? DiscountId { get; init; } = string.Empty;

    public string? Status { get; init; } = string.Empty;
}
