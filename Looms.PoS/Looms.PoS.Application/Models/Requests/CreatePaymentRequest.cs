using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Models.Requests;

public record CreatePaymentRequest
{
    public Guid OrderId { get; init; }
    public decimal Amount { get; init; }
    public PaymentMethod PaymentMethod { get; init; }
    public Guid? GiftCardId { get; init; }
    public decimal? Tip { get; init; }
}
