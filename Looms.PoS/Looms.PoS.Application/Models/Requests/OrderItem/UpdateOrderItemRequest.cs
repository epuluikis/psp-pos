namespace Looms.PoS.Application.Models.Requests;

public record UpdateOrderItemRequest
{                
    public int Quantity { get; init; }

    public string? DiscountId { get; init; }

    public string? VariationId { get; init; }
}