using Looms.PoS.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Looms.PoS.Domain.Daos;

public record OrderDao
{
    [Key]
    public Guid Id { get; init; }

    public Guid UserId { get; set; }

    public Guid BussinessId { get; init; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public Guid? DiscountId { get; set; } = null;

    public DiscountDao? Discount { get; set; } = null;

    public ICollection<PaymentDao> Payments { get; set; } = new List<PaymentDao>();

    public ICollection<RefundDao>? Refunds { get; set; }

    public ICollection<OrderItemDao> OrderItems { get; set; } = new List<OrderItemDao>();

    public bool IsDeleted { get; set; } = false;
}
