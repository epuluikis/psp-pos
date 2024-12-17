namespace Looms.PoS.Application.Models.Requests.Order;

public record CreateOrderRequest
{
    public string UserId { get; init; } = string.Empty;
    public string BusinessId { get; init; } = string.Empty;
}
