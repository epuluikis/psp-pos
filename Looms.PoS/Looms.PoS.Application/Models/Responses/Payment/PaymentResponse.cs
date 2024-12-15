using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Models.Responses.Payment;

public class PaymentResponse
{
    public Guid Id { get; init; }
    public Guid OrderId { get; init; }
    public decimal Amount { get; init; }
    public PaymentMethod PaymentMethod { get; init; }
    public string? GiftCardId { get; init; }
    public decimal Tip { get; init; }
    public PaymentStatus Status { get; init; }
    public bool IsDeleted { get; init; } = false;
}
