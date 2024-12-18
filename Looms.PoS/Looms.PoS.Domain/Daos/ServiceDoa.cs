using System.ComponentModel.DataAnnotations.Schema;

namespace Looms.PoS.Domain.Daos;

public record ServiceDao
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Category { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; init; }

    public int DurationMin { get; init; }

    public Guid BusinessId { get; init; }

    public Guid TaxId { get; init; }

    public bool IsDeleted { get; init; }


    public virtual BusinessDao Business { get; init; } = null!;
    public virtual TaxDao Tax { get; init; } = null!;
    public virtual ICollection<ReservationDao> Reservations { get; init; } = [];
    public virtual ICollection<OrderItemDao> OrderItems { get; init; } = [];
}
