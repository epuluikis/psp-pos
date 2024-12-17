using Looms.PoS.Application.Models.Responses.OrderItem;
using Looms.PoS.Application.Models.Responses.Payment;
using Looms.PoS.Application.Models.Responses.Refund;
using Looms.PoS.Domain.Enums;
using System.Text.Json.Serialization;

namespace Looms.PoS.Application.Models.Responses.Order;

public record OrderResponse
{
    public Guid Id { get; init; }

    public string BusinessName { get; init; } = string.Empty;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OrderStatus Status { get; init; } = OrderStatus.Pending;

    public IEnumerable<OrderItemResponse> OrderItems { get; init; } = new List<OrderItemResponse>();

    public IEnumerable<PaymentResponse> Payments { get; init; } = new List<PaymentResponse>();

    public IEnumerable<RefundResponse>? Refunds { get; init; } = new List<RefundResponse>();

    public Guid? DiscountId { get; init; } = null;

    public decimal TaxAmount { get; init; }

    public decimal TipAmount { get; init; }

    public decimal SubTotal => TotalAmount - TaxAmount - TipAmount;

    public decimal TotalAmount { get; init; }

    public decimal AmountPaid { get; init; }

    public decimal AmountDue => TotalAmount + TipAmount - AmountPaid;

    public decimal AmountRefunded { get; init; }
}
