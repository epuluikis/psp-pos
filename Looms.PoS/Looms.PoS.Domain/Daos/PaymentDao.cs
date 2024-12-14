using Looms.PoS.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Looms.PoS.Domain.Daos;

// TODO: reference Order
public record PaymentDao
{
    public Guid Id { get; init; }
    public Guid OrderId { get; init; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; init; }

    public PaymentMethod PaymentMethod { get; init; }
    public Guid? GiftCardId { get; init; }
    public decimal Tip { get; init; }
    public Guid? PaymentTerminalId { get; init; }
    public PaymentStatus Status { get; init; }
    public string? ExternalId { get; init; }
    public bool IsDeleted { get; init; }

    public virtual GiftCardDao? GiftCard { get; init; }
    public virtual PaymentTerminalDao? PaymentTerminal { get; init; }
}
