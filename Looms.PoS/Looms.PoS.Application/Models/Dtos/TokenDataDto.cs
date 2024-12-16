using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Models.Dtos;

public record TokenDataDto(string UserId, UserRole UserRole, string BusinessId)
{
    public string UserId { get; init; } = UserId;
    public UserRole UserRole { get; init; } = UserRole;
    public string BusinessId { get; init; } = BusinessId;
}
