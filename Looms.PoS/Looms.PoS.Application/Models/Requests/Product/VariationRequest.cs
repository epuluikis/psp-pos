namespace Looms.PoS.Application.Models.Requests.Product;

public record VariationRequest(string Name){
    public string Name = Name;
    public decimal? Price;
}