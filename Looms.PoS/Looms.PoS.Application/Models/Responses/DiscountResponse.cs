using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Models.Responses;
public class DiscountResponse
{
    public Guid Id { get; init; }
    public string? Name { get; set; } = string.Empty;
    public DiscountType DiscountType { get; set; }
    public decimal Value { get; set; }
    public DiscountTarget Target { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsDeleted { get; set; } = false;
}
