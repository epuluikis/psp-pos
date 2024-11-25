using Looms.PoS.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Looms.PoS.Domain.Daos;

public class RefundDao
{
    public Guid Id { get; init; }
    public Guid OrderId { get; init; }
    public Guid PaymentId { get; init; }
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; init; }
    public string RefundReason { get; init; } = string.Empty;
    public RefundStatus RefundStatus { get; init; } = RefundStatus.Pending;
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? ProcessedAt { get; init; } = null;
    public Guid UserId { get; init; }
}
