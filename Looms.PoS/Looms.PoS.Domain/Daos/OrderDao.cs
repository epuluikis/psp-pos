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

    public List<PaymentDao> Payments { get; init; } = [];

    public List<RefundDao>? Refunds { get; init; } = [];

    public List<OrderItemDao> OrderItems { get; init; } =[];

    public bool IsDeleted { get; init; } = false;
}
