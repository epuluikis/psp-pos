namespace Looms.PoS.Application.Models.Requests.GiftCard;

public record UpdateGiftCardRequest
{
    public string Code { get; init; } = string.Empty;
    public decimal CurrentBalance { get; init; }
    public string ExpiryDate { get; init; } = string.Empty;
    public bool IsActive { get; init; }
}
