namespace Looms.PoS.Application.Models.Requests;

public record CreateOrderItemRequest
{
    public string OrderId { get; init; } 
    
    public string? ProductId { get; init; }
    
    public string? VariationId { get; init; }
    
    public string? ServiceId { get; init; }
    
    public int Quantity { get; init; }

    public string? DiscountId { get; init; }
}