using System.ComponentModel.DataAnnotations.Schema;

namespace Looms.PoS.Domain.Daos;

public record ProductVariationDao
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public string VariationName { get; init; } = string.Empty;
    public bool IsDeleted { get; init; }
}
