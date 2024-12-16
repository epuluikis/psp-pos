namespace Looms.PoS.Application.Models.Responses;

public class OrderItemResponse {
    public Guid Id { get; init; }

    public Guid? ProductId { get; init; }

    public string? ProductName { get; init; } = string.Empty;

    public Guid? VariationId { get; init; }

    public string? VariationName { get; init; } = string.Empty;
    
    public Guid? ReservationId { get; init; }
    
    public string? ServiceName { get; init; } = string.Empty;
        
    public int Quantity { get; init; }

    public decimal Price { get; init; }
    
    public decimal Tax { get; init; }
    
    public decimal Total { get; init; }
}