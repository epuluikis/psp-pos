namespace Looms.PoS.Application.Models.Requests.PaymentTerminal;

public record UpdatePaymentTerminalRequest
{
    public string Name { get; init; } = string.Empty;
    public string ExternalId { get; init; } = string.Empty;
    public string PaymentProviderId { get; init; } = string.Empty;
    public bool IsActive { get; init; }
}
