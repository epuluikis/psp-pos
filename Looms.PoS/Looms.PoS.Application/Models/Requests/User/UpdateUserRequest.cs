namespace Looms.PoS.Application.Models.Requests.User;

public record UpdateUserRequest
{
    public string Name { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
    public string BusinessId { get; init; } = string.Empty;
}
