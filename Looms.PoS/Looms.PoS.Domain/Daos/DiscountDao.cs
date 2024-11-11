using Looms.PoS.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Looms.PoS.Domain.Daos;

public record DiscountDao
{
    public Guid Id { get; init; }
    public string? Name { get; set; } = string.Empty;
    public DiscountType DiscountType { get; set; }
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal Value { get; set; }
    public DiscountTarget Target { get; set; } = DiscountTarget.Order;
    public Guid? ProductId { get; set; } = Guid.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsDeleted { get; set; } = false;
}
