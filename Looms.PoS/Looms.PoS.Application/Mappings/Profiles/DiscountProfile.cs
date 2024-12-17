using AutoMapper;
using Looms.PoS.Application.Models.Requests.Discount;
using Looms.PoS.Application.Utilities.Helpers;
using Looms.PoS.Application.Models.Responses.Discount;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Mappings.Profiles;

public class DiscountProfile : Profile
{
    public DiscountProfile()
    {
        CreateMap<CreateDiscountRequest, DiscountDao>(MemberList.Source)
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateTimeHelper.ConvertToUtc(src.StartDate)))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => DateTimeHelper.ConvertToUtc(src.EndDate)));

        CreateMap<UpdateDiscountRequest, DiscountDao>(MemberList.Source)
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateTimeHelper.ConvertToUtc(src.StartDate)))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => DateTimeHelper.ConvertToUtc(src.EndDate)));

        CreateMap<DiscountDao, DiscountResponse>()
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => DateTimeHelper.ConvertToLocal(src.StartDate)))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => DateTimeHelper.ConvertToLocal(src.EndDate)));
    }
}
