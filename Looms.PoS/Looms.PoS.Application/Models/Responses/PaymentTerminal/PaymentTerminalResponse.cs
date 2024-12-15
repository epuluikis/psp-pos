namespace Looms.PoS.Application.Models.Responses.PaymentTerminal;

public class PaymentTerminalResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string ExternalId { get; init; } = string.Empty;
    public Guid BusinessId { get; init; }
    public bool IsActive { get; init; }
    public bool IsDeleted { get; init; }
}
