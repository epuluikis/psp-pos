namespace Looms.PoS.Application.Models.Requests;

public record CreateBusinessRequest
{
    public string Name { get; init; } = string.Empty;
    public string Owner { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string? PhoneNumber { get; init; }
}
