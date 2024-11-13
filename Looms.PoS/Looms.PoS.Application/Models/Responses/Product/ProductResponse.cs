namespace Looms.PoS.Application.Models.Responses.Product;

//TODO: is this really correct??
public class ProductResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public Guid TaxId { get; init; }
    //TODO: Should also contain BusinessId
    // public Guid BusinessId { get; init; }
    public bool IsDeleted { get; init; }

}
