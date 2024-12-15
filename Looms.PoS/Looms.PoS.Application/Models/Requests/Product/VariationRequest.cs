namespace Looms.PoS.Application.Models.Requests.Product;

public record VariationRequest(String Name){
    public String Name = Name;
    public decimal? Price;
}