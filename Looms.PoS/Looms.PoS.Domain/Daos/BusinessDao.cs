namespace Looms.PoS.Domain.Daos;

public record BusinessDao
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string OwnerName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string PhoneNumber { get; init; } = string.Empty;

    public int StartHour { get; init; }

    public int EndHour { get; init; }

    public bool IsDeleted { get; init; }


    public virtual ICollection<UserDao> Users { get; } = [];
    public virtual ICollection<PaymentProviderDao> PaymentProviders { get; init; } = [];
    public virtual ICollection<ServiceDao> Services { get; init; } = [];
    public virtual ICollection<DiscountDao> Discounts { get; init; } = [];
    public virtual ICollection<GiftCardDao> GiftCards { get; init; } = [];
    public virtual ICollection<TaxDao> Taxes { get; init; } = [];
    public virtual ICollection<ProductDao> Products { get; init; } = [];
    public virtual ICollection<OrderDao> Orders { get; init; } = [];
}
