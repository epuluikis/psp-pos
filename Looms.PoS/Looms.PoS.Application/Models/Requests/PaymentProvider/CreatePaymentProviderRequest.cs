using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Models.Requests.PaymentProvider;

public record CreatePaymentProviderRequest
{
    public string Name { get; init; } = string.Empty;
    public PaymentProviderType Type { get; init; }
    public string ExternalId { get; init; } = string.Empty;
    public string ApiSecret { get; init; } = string.Empty;
    public string WebhookSecret { get; init; } = string.Empty;
    public bool IsActive { get; init; }
}
