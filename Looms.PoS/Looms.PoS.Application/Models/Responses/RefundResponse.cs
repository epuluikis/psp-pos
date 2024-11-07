using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Models.Responses;
public class RefundResponse
{
    public Guid Id { get; init; }
    public Guid OrderId { get; init; }
    public Guid PaymentId { get; init; }
    public decimal Amount { get; init; }
    public string RefundReason { get; set; } = string.Empty;
    public RefundStatus RefundStatus { get; set; } 
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? ProcessedAt { get; set; } = null;
    public Guid UserId { get; init; }
}