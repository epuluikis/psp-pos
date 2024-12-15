using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Domain.Daos;

public record PaymentProviderDao
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public PaymentProviderType Type { get; init; }
    public string ExternalId { get; init; } = string.Empty;
    public string ApiSecret { get; init; } = string.Empty;
    public string WebhookSecret { get; init; } = string.Empty;
    public Guid BusinessId { get; init; }
    public bool IsActive { get; init; }
    public bool IsDeleted { get; init; }

    public virtual BusinessDao? Business { get; init; } = null;
    public virtual ICollection<PaymentTerminalDao> PaymentTerminals { get; } = [];
}
