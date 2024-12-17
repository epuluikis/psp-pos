using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Domain.Daos;

public record OrderDao
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }

    public Guid BusinessId { get; init; }

    public OrderStatus Status { get; init; } = OrderStatus.Pending;

    public Guid? DiscountId { get; init; }

    public bool IsDeleted { get; init; }


    public virtual UserDao User { get; init; } = null!;
    public virtual BusinessDao Business { get; init; } = null!;
    public virtual DiscountDao? Discount { get; init; }
    public virtual ICollection<PaymentDao> Payments { get; init; } = [];
    public virtual ICollection<RefundDao> Refunds { get; init; } = [];
    public virtual ICollection<OrderItemDao> OrderItems { get; init; } = [];
}
