using System.ComponentModel.DataAnnotations.Schema;

namespace Looms.PoS.Domain.Daos;

// TODO: Uncomment for reliationships between OrderItemDao and other Daos

public record OrderItemDao
{
    public Guid Id { get; init; }

    public Guid OrderId { get; init; }
    
    public OrderDao Order { get; init; }

    public Guid? ProductId { get; init; }

    public ProductDao? Product { get; init; }

    public Guid? ProductVariationId { get; init; }

    public ProductVariationDao? ProductVariation { get; init; }

    public Guid? ServiceId { get; init; }

    //public ServiceDao? Service { get; init; }

    public Guid? DiscountId { get; init; }

    public DiscountDao? Discount { get; init; }

    public int Quantity { get; init; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; init; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Tax { get; init; }

    public bool IsDeleted { get; init; }
}