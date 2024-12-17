namespace Looms.PoS.Domain.Filters.Order;

public record GetAllOrdersFilter
{
    public string? Status { get; init; }
    public string? UserId { get; init; }
}
