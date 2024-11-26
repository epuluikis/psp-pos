namespace Looms.PoS.Application.Models.Requests.Product;

public record UpdateProductVariationRequest
{
    //TODO: is this the way to link it to Product?
    public Guid ProductId { get; init; }
    public String Name { get; init; } = string.Empty;
    public String Category { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public decimal QuantityInStock { get; init; }
}
