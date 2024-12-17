using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Models.Responses.Refund;

public record RefundResponse
{
    public Guid Id { get; init; }
    public Guid OrderId { get; init; }
    public Guid PaymentId { get; init; }
    public decimal Amount { get; init; }
    public string RefundReason { get; init; } = string.Empty;
    public RefundStatus RefundStatus { get; init; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime? ProcessedAt { get; init; } = null;
    public Guid UserId { get; init; }
}
