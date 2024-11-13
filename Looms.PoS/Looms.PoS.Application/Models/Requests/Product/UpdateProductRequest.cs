namespace Looms.PoS.Application.Models.Requests.Product;

public record UpdateProductRequest
{
    public String Name { get; init; } = string.Empty;
    public String Category { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public decimal QuantityInStock { get; init; }
    public String Description { get; init; } = string.Empty;
//TODO: should we need a variation id or 2 params for variation
    public Guid VariationId { get; init; }
}
