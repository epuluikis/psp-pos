namespace Looms.PoS.Application.Models.Requests.Product;

public record CreateProductRequest
{
    public String Name { get; init; } = string.Empty;
    public String Category { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public decimal QuantityInStock { get; init; }
    public String Description { get; init; } = string.Empty;
//TODO: add an IEnnumerable to insert variations
    public Guid VariationId { get; init; }
}
