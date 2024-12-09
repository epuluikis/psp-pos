using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Domain.Daos;

public record OrderDao
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }
    
    public UserDao User { get; init; } 

    public Guid BussinessId { get; init; }

    public BusinessDao Business { get; init; } = null!;

    public OrderStatus Status { get; init; } = OrderStatus.Pending;

    public Guid? DiscountId { get; init; } = null;

    public DiscountDao? Discount { get; init; } = null;

    public ICollection<PaymentDao> Payments { get; init; } = new List<PaymentDao>();

    public ICollection<RefundDao>? Refunds { get; init; } = new List<RefundDao>();

    public ICollection<OrderItemDao> OrderItems { get; init; } = new List<OrderItemDao>();

    public bool IsDeleted { get; init; } = false;
}
