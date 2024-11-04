namespace Looms.PoS.Application.Models.Responses;

public class BusinessResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Owner { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public bool IsDeleted { get; init; } = false;
}
