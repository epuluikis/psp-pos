using System.ComponentModel.DataAnnotations.Schema;

namespace Looms.PoS.Domain.Daos;

public record ProductVariationDao
{
    public Guid Id { get; init; }

    public Guid ProductId { get; init; }

    public decimal? Price { get; init; }

    public int Quantity { get; init; }

    public string VariationName { get; init; } = string.Empty;

    public bool IsDeleted { get; init; }


    public virtual ProductDao Product { get; init; } = null!;
    public virtual ICollection<OrderItemDao> OrderItems { get; init; } = [];
}
