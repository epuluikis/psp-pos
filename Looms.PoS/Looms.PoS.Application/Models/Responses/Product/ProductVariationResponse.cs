namespace Looms.PoS.Application.Models.Responses.Product;

public record ProductVariationResponse
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public decimal? Price { get; init; }
    public int Quantity { get; init; } = 0;
    public string VariationName { get; init; } = string.Empty;
    public bool IsDeleted { get; init; }
}
