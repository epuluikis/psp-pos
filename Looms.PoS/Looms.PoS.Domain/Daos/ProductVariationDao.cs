using System.ComponentModel.DataAnnotations.Schema;

namespace Looms.PoS.Domain.Daos;

public record ProductVariationDao
{
    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    //TODO: in the class diagram this doesnt have price, but in the API specification it does (as optional, but still)
    public string VariationName { get; init; } = string.Empty;
    public bool IsDeleted { get; init; }
}
