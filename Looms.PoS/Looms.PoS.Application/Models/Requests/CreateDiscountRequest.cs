using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Models.Requests;
public record CreateDiscountRequest
{
    public string? Name { get; init; } = string.Empty;
    public DiscountType DiscountType { get; init; }
    public decimal Value { get; init; }
    public DiscountTarget DiscountTarget { get; init; }
    public DateTime StartDate { get; init; } 
    public DateTime EndDate { get; init; }
}
