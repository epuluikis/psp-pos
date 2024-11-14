namespace Looms.PoS.Application.Models.Responses;

public class DiscountResponse
{
    public Guid Id { get; init; }
    public string? Name { get; set; } = string.Empty;
    public string DiscountType { get; set; }
    public decimal Value { get; set; }
    public string DiscountTarget { get; set; }
    public Guid? ProductId { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public bool IsDeleted { get; set; } = false;
}
