using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Domain.Daos;

public record TaxDao
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int Percentage { get; init; }
    public TaxCategory TaxCategory { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public bool IsDeleted { get; init; } = false;
    public virtual ICollection<ServiceDao> Services { get; init; } = [];
}
