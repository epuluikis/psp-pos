namespace Looms.PoS.Application.Models.Requests.ProductVariation;

public record CreateProductVariationRequest
{
    public Guid ProductId { get; init; }
    public String Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public decimal QuantityInStock { get; init; }
    public String Description { get; init; } = string.Empty;
}