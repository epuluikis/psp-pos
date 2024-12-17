namespace Looms.PoS.Application.Models.Requests.Order;

public record CreateOrderRequest
{
    public string? DiscountId { get; init; }
}
