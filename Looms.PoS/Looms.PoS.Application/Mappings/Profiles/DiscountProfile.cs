using AutoMapper;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Application.Utilities;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.Profiles;

public class DiscountProfile : Profile
{
    public DiscountProfile()
    {
        CreateMap<CreateDiscountRequest, DiscountDao>(MemberList.Source)
            .ForMember(dest => dest.Target, opt => opt.MapFrom(src => src.DiscountTarget))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateTimeHelper.ConvertToUtc(src.StartDate)))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => DateTimeHelper.ConvertToUtc(src.EndDate)));

        CreateMap<UpdateDiscountRequest, DiscountDao>(MemberList.Source)
            .ForMember(dest => dest.Target, opt => opt.MapFrom(src => src.DiscountTarget))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateTimeHelper.ConvertToUtc(src.StartDate)))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => DateTimeHelper.ConvertToUtc(src.EndDate)));

        CreateMap<DiscountDao, DiscountResponse>()
            .ForMember(dest => dest.DiscountTarget, opt => opt.MapFrom(src => src.Target))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateTimeHelper.ConvertToLocal(src.StartDate)))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => DateTimeHelper.ConvertToLocal(src.EndDate)));
    }
}
