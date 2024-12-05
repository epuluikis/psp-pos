namespace Looms.PoS.Application.Models.Responses;

public record ServiceResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int DurationMin { get; init; }
    public Guid BusinessId { get; init; }
    public Guid TaxId { get; init; }
    public bool IsDeleted { get; init; } = false;
}
