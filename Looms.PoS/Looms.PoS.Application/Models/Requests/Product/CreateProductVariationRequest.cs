namespace Looms.PoS.Application.Models.Requests.Product;

public record CreateProductVariationRequest
{
    public Guid ProductId { get; init; }
    public string Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public decimal QuantityInStock { get; init; }
    public string Description { get; init; } = string.Empty;
}
