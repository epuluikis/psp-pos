namespace Looms.PoS.Application.Models.Requests.ProductVariation;

public record UpdateProductVariationRequest
{
    public string ProductId { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public decimal QuantityInStock { get; init; }
}
