using AutoMapper;
using Looms.PoS.Application.Models.Requests.Business;
using Looms.PoS.Application.Models.Responses.Business;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.Profiles;

public class BusinessProfile : Profile
{
    public BusinessProfile()
    {
        CreateMap<CreateBusinessRequest, BusinessDao>(MemberList.Source)
            .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Owner));

        CreateMap<UpdateBusinessRequest, BusinessDao>(MemberList.Source);

        CreateMap<BusinessDao, BusinessResponse>()
            .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.OwnerName));
    }
}
