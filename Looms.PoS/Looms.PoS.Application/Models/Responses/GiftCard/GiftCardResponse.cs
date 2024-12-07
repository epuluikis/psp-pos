namespace Looms.PoS.Application.Models.Responses.GiftCard;

public class GiftCardResponse
{
    public Guid Id { get; init; }
    public string Code { get; init; } = string.Empty;
    public decimal InitialBalance { get; init; }
    public decimal CurrentBalance { get; init; }
    public DateTime ExpiryDate { get; init; }
    public bool IsActive { get; init; }
    public Guid IssuedBy { get; init; }
    public Guid BusinessId { get; init; }
    public bool IsDeleted { get; init; }
}
