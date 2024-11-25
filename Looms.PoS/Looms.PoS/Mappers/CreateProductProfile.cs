using AutoMapper;
using Looms.PoS.Application.Models.Requests.Product;
using Looms.PoS.Domain.Daos;

public class CreateProductProfile : Profile
{
    public CreateProductProfile()
    {
        // Map from CreateProductRequest to ProductDao
        CreateMap<CreateProductRequest, ProductDao>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

        // Map from CreateProductRequest to ProductStockDao
        CreateMap<CreateProductRequest, ProductStockDao>()
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.QuantityInStock));

        // Map from CreateProductRequest to IEnumerable<ProductVariationDao>
        CreateMap<CreateProductRequest, IEnumerable<ProductVariationDao>>()
            .ConvertUsing(src => src.VariationRequest.Select(v => new ProductVariationDao
            {
                VariationName = v.Name,
                Price = v.Price
            }));
    }
}
