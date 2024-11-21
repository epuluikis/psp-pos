using Looms.PoS.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Looms.PoS.Domain.Daos;

public record OrderItemDao
{
    public Guid Id { get; init; }

    public Guid OrderId { get; init; }
    
    public OrderDao Order { get; init; }

    public Guid? ProductId { get; set; }

    public Guid? VariationId { get; set; }

    public Guid? ServiceId { get; set; }

    public Guid? DiscountId { get; set; }

    public DiscountDao? Discount { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal TaxAmount { get; set; }

    public bool IsDeleted { get; set; }
}