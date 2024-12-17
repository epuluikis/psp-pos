namespace Looms.PoS.Application.Models.Requests.OrderItem;

public record CreateOrderItemRequest
{
    public string? ProductId { get; init; }

    public string? ProductVariationId { get; init; }

    public string? ServiceId { get; init; }

    public int Quantity { get; init; }

    public string? DiscountId { get; init; }
}
