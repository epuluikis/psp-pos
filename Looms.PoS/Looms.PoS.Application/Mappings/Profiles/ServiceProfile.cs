using AutoMapper;
using Looms.PoS.Application.Models.Requests.Service;
using Looms.PoS.Application.Models.Responses.Service;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.Profiles;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<CreateServiceRequest, ServiceDao>(MemberList.Source)
            .ForMember(dest => dest.BusinessId, opt => opt.MapFrom(src => Guid.Parse(src.BusinessId)));

        CreateMap<UpdateServiceRequest, ServiceDao>(MemberList.Source);

        CreateMap<ServiceDao, ServiceResponse>();
    }
}
