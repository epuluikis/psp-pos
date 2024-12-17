using System.ComponentModel.DataAnnotations.Schema;

namespace Looms.PoS.Domain.Daos;

public record GiftCardDao
{
    public Guid Id { get; init; }

    public string Code { get; init; } = string.Empty;


    [Column(TypeName = "decimal(10,2)")]
    public decimal InitialBalance { get; init; }


    [Column(TypeName = "decimal(10,2)")]
    public decimal CurrentBalance { get; init; }

    public DateTime ExpiryDate { get; init; }

    public bool IsActive { get; init; }

    public Guid IssuedById { get; init; }

    public Guid BusinessId { get; init; }

    public bool IsDeleted { get; init; }


    public virtual UserDao IssuedBy { get; init; } = null!;
    public virtual BusinessDao Business { get; init; } = null!;
    public virtual ICollection<PaymentDao> Payments { get; init; } = [];
}
