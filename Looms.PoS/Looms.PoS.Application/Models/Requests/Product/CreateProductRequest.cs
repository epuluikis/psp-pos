using Looms.PoS.Application.Features.Business.Queries.GetBusinesses;

namespace Looms.PoS.Application.Models.Requests.Product;

public record CreateProductRequest
{
    public String Name { get; init; } = string.Empty;
    public String Category { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public decimal QuantityInStock { get; init; }
    public String Description { get; init; } = string.Empty;
    public Variation Variation { get; init; } = new Variation(string.Empty, null);
    //TODO: maybe it would be better to reference the variation, because we have it in the class diagram
    // public Guid VariationId { get; init; }
}