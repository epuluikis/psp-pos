using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Models.Responses.PaymentProvider;

public record PaymentProviderResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public PaymentProviderType Type { get; init; }
    public string ExternalId { get; init; } = string.Empty;
    public Guid BusinessId { get; init; }
    public bool IsActive { get; init; }
    public bool IsDeleted { get; init; }
}
