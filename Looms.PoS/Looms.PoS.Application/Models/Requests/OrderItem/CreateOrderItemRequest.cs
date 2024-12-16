namespace Looms.PoS.Application.Models.Requests;

public record CreateOrderItemRequest
{    
    public string? ProductId { get; init; }
    
    public string? ProductVariationId { get; init; }
    
    public string? ReservationId { get; init; }
    
    public int Quantity { get; init; }

    public string? DiscountId { get; init; }
}