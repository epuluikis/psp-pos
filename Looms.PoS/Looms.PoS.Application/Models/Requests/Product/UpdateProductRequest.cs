namespace Looms.PoS.Application.Models.Requests.Product;

public record UpdateProductRequest
{
    public string Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public decimal QuantityInStock { get; init; }
    public string Description { get; init; } = string.Empty;
}
