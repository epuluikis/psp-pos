namespace Looms.PoS.Domain.Daos;

public enum DiscountType
{
    Percentage,
    FixedAmount
}

public enum DiscountTarget
{
    Order, 
    OrderItem,
    Both
}

public class DiscountDao
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
