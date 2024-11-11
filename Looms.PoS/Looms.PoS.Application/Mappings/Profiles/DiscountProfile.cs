using AutoMapper;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.Profiles;

public class DiscountProfile : Profile
{
    public DiscountProfile()
    {
        CreateMap<CreateDiscountRequest, DiscountDao>(MemberList.Source)
            .ForMember(dest => dest.Target, opt => opt.MapFrom(src => src.DiscountTarget));
        CreateMap<UpdateDiscountRequest, DiscountDao>(MemberList.Source)
            .ForMember(dest => dest.Target, opt => opt.MapFrom(src => src.DiscountTarget));
        CreateMap<DiscountDao, DiscountResponse>()
            .ForMember(dest => dest.DiscountTarget, opt => opt.MapFrom(src => src.Target));
    }
}
