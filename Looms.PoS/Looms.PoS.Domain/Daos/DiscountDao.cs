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
    public Guid? ProductId { get; init; } = Guid.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public bool IsDeleted { get; init; } = false;
}
