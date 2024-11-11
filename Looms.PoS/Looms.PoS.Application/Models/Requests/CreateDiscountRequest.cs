using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Models.Requests;
public record CreateDiscountRequest
{
    public string? Name { get; init; } = string.Empty;
    public DiscountType DiscountType { get; init; }
    public decimal Value { get; init; }
    public DiscountTarget DiscountTarget { get; init; }
    public Guid? ProductId { get; init; }
    public DateTime StartDate { get; init; } 
    public DateTime EndDate { get; init; }
}
