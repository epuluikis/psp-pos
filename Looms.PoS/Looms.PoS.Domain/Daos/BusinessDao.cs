namespace Looms.PoS.Domain.Daos;

public record BusinessDao
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string OwnerName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string PhoneNumber { get; init; } = string.Empty;
    public bool IsDeleted { get; init; } = false;

    public virtual ICollection<UserDao> Users { get; } = [];
    public virtual ICollection<PaymentProviderDao> PaymentProviders { get; init; } = [];
}
