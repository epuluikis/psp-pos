namespace Looms.PoS.Application.Models.Requests.ProductVariation;

public record UpdateProductVariationRequest
{
    public Guid ProductId { get; init; }
    public String Name { get; init; } = string.Empty;
    public String Category { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public decimal QuantityInStock { get; init; }
}
