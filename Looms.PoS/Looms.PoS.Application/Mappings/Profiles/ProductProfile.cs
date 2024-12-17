using AutoMapper;
using Looms.PoS.Application.Models.Requests.Product;
using Looms.PoS.Application.Models.Responses.Product;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductRequest, ProductDao>(MemberList.Source)
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.QuantityInStock));

        CreateMap<UpdateProductRequest, ProductDao>(MemberList.Source)
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.QuantityInStock));

        CreateMap<ProductDao, ProductResponse>()
            .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.Quantity));
    }
}
