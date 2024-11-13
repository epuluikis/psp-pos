namespace Looms.PoS.Application.Models.Requests;

public record CreateRefundRequest
{
    public string OrderId { get; init; } = string.Empty;
    public string PaymentId { get; init; } = string.Empty;
    public decimal Amount { get; init; } = 0;
    public string RefundReason { get; set; } = string.Empty;
}
