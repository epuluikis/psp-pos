using System.ComponentModel.DataAnnotations.Schema;

namespace Looms.PoS.Domain.Daos;

// TODO: reference User
// Can't be managed through API as not documented in the API contract
public record GiftCardDao
{
    public Guid Id { get; init; }

    public string Code { get; init; } = string.Empty;

    [Column(TypeName = "decimal(10,2)")]
    public decimal InitialBalance { get; init; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal CurrentBalance { get; set; }

    public DateTime ExpiryDate { get; init; }

    public bool IsActive { get; init; }

    public Guid IssuedBy { get; init; }

    public Guid BusinessId { get; init; }

    public bool IsDeleted { get; init; }


    public BusinessDao? Business { get; init; }
}
