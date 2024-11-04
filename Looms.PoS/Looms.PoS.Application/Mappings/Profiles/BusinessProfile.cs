using AutoMapper;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.Profiles;

public class BusinessProfile : Profile
{
    public BusinessProfile()
    {
        CreateMap<CreateBusinessRequest, BusinessDao>(MemberList.Source)
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Owner));

        CreateMap<BusinessDao, BusinessResponse>()
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.OwnerName));
    }
}
