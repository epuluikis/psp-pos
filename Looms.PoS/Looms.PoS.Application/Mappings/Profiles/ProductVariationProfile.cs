using AutoMapper;
using Looms.PoS.Application.Models.Requests.ProductVariation;
using Looms.PoS.Application.Models.Responses.ProductVariation;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.Profiles;

public class ProductVariationProfile : Profile
{
    public ProductVariationProfile()
    {
        CreateMap<CreateProductVariationRequest, ProductVariationDao>(MemberList.Source)
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.QuantityInStock))
            .ForMember(dest => dest.VariationName, opt => opt.MapFrom(src => src.Name));

        CreateMap<ProductVariationDao, ProductVariationResponse>();
    }
}
