using System.ComponentModel.DataAnnotations.Schema;

namespace Looms.PoS.Domain.Daos;

public enum RefundStatus
{
    Pending,
    Completed,
    Rejected
}

public class RefundDao
{
    public Guid Id { get; init; }
    public Guid OrderId { get; init; }
    public Guid PaymentId { get; init; }
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; init; }
    public string RefundReason { get; set; } = string.Empty;
    public RefundStatus RefundStatus { get; set; } 
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? ProcessedAt { get; set; } = null;
    public Guid UserId { get; init; }
}
