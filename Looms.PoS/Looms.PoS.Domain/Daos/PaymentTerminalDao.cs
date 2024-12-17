namespace Looms.PoS.Domain.Daos;

public record PaymentTerminalDao
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string ExternalId { get; init; } = string.Empty;

    public Guid PaymentProviderId { get; init; }

    public bool IsActive { get; init; }

    public bool IsDeleted { get; init; }


    public virtual PaymentProviderDao PaymentProvider { get; init; } = null!;
    public virtual ICollection<PaymentDao> Payments { get; init; } = [];
}
