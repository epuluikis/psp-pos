using Looms.PoS.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Looms.PoS.Domain.Daos;

// TODO: reference Order
public record PaymentDao
{
    public Guid Id { get; init; }
    public Guid OrderId { get; init; }

    public OrderDao Order { get; init; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; init; }

    public PaymentMethod PaymentMethod { get; init; }
    public Guid? GiftCardId { get; init; }
    public decimal Tip { get; init; }
    public bool IsDeleted { get; init; }


    public GiftCardDao? GiftCard { get; init; }
}
