using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Domain.Daos;

public record OrderDao
{
    public Guid Id { get; init; }

    public Guid UserId { get; init; }
    
    public Guid BussinessId { get; init; }

    public OrderStatus Status { get; init; } = OrderStatus.Pending;

    public Guid? DiscountId { get; init; } = null;

    public bool IsDeleted { get; init; } = false;


    public virtual UserDao User { get; init; } 

    public virtual BusinessDao Business { get; init; } = null!;

    public virtual DiscountDao? Discount { get; init; } = null;

    public virtual ICollection<PaymentDao> Payments { get; init; } = [];
    
    public virtual ICollection<RefundDao>? Refunds { get; init; } = [];

    public virtual ICollection<OrderItemDao> OrderItems { get; init; } =[];
}
