using System.ComponentModel.DataAnnotations.Schema;

namespace Looms.PoS.Domain.Daos;

public record ProductStockDao
{
    public Guid Id { get; init; }
    public Guid VariationId { get; init; }
    public Guid ProductId { get; init; }
    public int Quantity { get; set; } = 0;
    public bool IsDeleted { get; init; }
}
