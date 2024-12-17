namespace Looms.PoS.Application.Models.Requests.Business;

public record UpdateBusinessRequest
{
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string? PhoneNumber { get; init; }
    public int StartHour { get; init; }
    public int EndHour { get; init; }
}
