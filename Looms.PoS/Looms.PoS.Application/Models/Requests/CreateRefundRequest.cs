using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Models.Requests;
public record CreateRefundRequest
{
    public string OrderId { get; init; } = string.Empty;
    public string PaymentId { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public string RefundReason { get; set; } = string.Empty;
    public string UserId { get; init; } = string.Empty;
}
