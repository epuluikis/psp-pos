using System.ComponentModel.DataAnnotations.Schema;

namespace Looms.PoS.Domain.Daos;

public record ProductDao
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    [Column(TypeName = "decimal(10,2)")]
    public decimal? Price { get; init; }
    public int Quantity { get; init; } = 0;
    public Guid TaxId { get; init; }
    public TaxDao Tax { get; init; }
    public Guid BusinessId { get; init; }
    public bool IsDeleted { get; init; }
}
