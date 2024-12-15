using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Models.Requests.Payment;

public record CreatePaymentRequest
{
    public string OrderId { get; init; } = string.Empty;
    public decimal Amount { get; init; }
    public PaymentMethod PaymentMethod { get; init; }
    public string? GiftCardCode { get; init; }
    public decimal? Tip { get; init; }
    public string? PaymentTerminalId { get; init; }
}
