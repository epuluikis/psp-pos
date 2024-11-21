namespace Looms.PoS.Application.Models.Requests.Service;

public record CreateServiceRequest
{
    public string Name { get; init; } = string.Empty;
    public string Category { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int DurationMin { get; init; }
    public string BusinessId { get; init; } = string.Empty;
    public string? Description { get; init; }
}
