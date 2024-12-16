using System.ComponentModel.DataAnnotations.Schema;

namespace Looms.PoS.Domain.Daos;

public record OrderItemDao
{
    public Guid Id { get; init; }

    public Guid OrderId { get; init; }
    
    public Guid? ProductId { get; init; }

    public Guid? ProductVariationId { get; init; }

    public Guid? ReservationId { get; init; }

    public Guid? DiscountId { get; init; }

    public int Quantity { get; init; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; init; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Tax { get; init; }

    public bool IsDeleted { get; init; }


    public virtual OrderDao Order { get; init; }

    public virtual ProductDao? Product { get; init; }

    public virtual ProductVariationDao? ProductVariation { get; init; }

    public virtual ReservationDao? Reservation { get; init; }
    
    public virtual DiscountDao? Discount { get; init; }

}