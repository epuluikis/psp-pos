using AutoMapper;
using Looms.PoS.Application.Models.Requests.Service;
using Looms.PoS.Application.Models.Responses.Service;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.Profiles;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<CreateServiceRequest, ServiceDao>(MemberList.Source);

        CreateMap<UpdateServiceRequest, ServiceDao>(MemberList.Source);

        CreateMap<ServiceDao, ServiceResponse>();
    }
}
