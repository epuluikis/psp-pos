namespace Looms.PoS.Application.Models.Requests.Tax;

public record CreateTaxRequest
{
    public int Percentage { get; init; }
    public string TaxCategory { get; init; } = string.Empty;
    public string StartDate { get; init; } = string.Empty;
    public string? EndDate { get; init; }
}
