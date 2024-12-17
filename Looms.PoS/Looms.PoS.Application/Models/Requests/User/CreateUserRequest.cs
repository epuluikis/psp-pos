namespace Looms.PoS.Application.Models.Requests.User;

public record CreateUserRequest
{
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Role { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}
