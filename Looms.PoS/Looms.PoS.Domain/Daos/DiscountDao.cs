using Looms.PoS.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Looms.PoS.Domain.Daos;

public record DiscountDao
{
    public Guid Id { get; init; }

    public string? Name { get; init; } = string.Empty;

    public DiscountType DiscountType { get; init; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Value { get; init; }

    public DiscountTarget Target { get; init; } = DiscountTarget.Order;

    public Guid? ProductId { get; init; }

    public Guid? BusinessId { get; init; }

    public DateTime StartDate { get; init; }

    public DateTime EndDate { get; init; }

    public bool IsDeleted { get; init; }


    public virtual ProductDao? Product { get; init; }
    public virtual BusinessDao? Business { get; init; }
    public virtual ICollection<OrderDao> Orders { get; init; } = [];
    public virtual ICollection<OrderItemDao> OrderItems { get; init; } = [];
}
