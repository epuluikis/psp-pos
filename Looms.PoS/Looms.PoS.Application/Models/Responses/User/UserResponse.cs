using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Models.Responses.User;

public record UserResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public UserRole Role { get; init; }
    public Guid BusinessId { get; init; }
    public bool IsDeleted { get; init; } = false;
}
