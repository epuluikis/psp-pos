namespace Looms.PoS.Application.Models.Requests.Product;

//TODO: idk where to put this, so im having it here since both create and update use the same structure
public record Variation(string Name, float? Price);