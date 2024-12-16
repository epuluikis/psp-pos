namespace Looms.PoS.Application.Models.Requests.Product;

public record UpdateProductRequest
{
    public String Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public decimal QuantityInStock { get; init; }
    public String Description { get; init; } = string.Empty;
    public IEnumerable<VariationRequest> VariationRequest { get; init; } = [];
}
