using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Models.Responses.Auth;

public record LoginResponse
{
    public string Token { get; init; } = string.Empty;
    public DateTime Expires { get; init; }
    public UserRole Role { get; init; }
    public Guid UserId { get; init; }
    public Guid BusinessId { get; init; }
}
