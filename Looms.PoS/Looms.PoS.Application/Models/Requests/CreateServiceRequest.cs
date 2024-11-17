namespace Looms.PoS.Application.Models.Requests;

public record CreateServiceRequest
{
    public string Name { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public decimal DurationMin { get; init; }
    public string? Description { get; init; }
}
