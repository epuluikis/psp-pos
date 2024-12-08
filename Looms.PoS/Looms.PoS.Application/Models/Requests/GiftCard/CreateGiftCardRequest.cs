namespace Looms.PoS.Application.Models.Requests.GiftCard;

public record CreateGiftCardRequest
{
    public string Code { get; init; } = string.Empty;
    public decimal InitialBalance { get; init; }
    public string ExpiryDate { get; init; } = string.Empty;
    public bool IsActive { get; init; }
}
