namespace Looms.PoS.Application.Models.Requests.Product;

public record UpdateProductVariationRequest
{
    public string ProductId { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public decimal QuantityInStock { get; init; }
}
