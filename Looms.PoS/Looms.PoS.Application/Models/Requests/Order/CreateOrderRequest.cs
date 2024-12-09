namespace Looms.PoS.Application.Models.Requests;

public record CreateOrderRequest
{
    public string UserId { get; init; } = string.Empty;
    public string BusinessId { get; init; } = string.Empty;
}