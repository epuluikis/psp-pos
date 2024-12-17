namespace Looms.PoS.Application.Models.Requests.Product;

public record CreateProductRequest
{
    public string Name { get; init; } = string.Empty;
    public string? TaxId { get; init; }
    public decimal Price { get; init; }
    public int QuantityInStock { get; init; }
    public string Description { get; init; } = string.Empty;
}