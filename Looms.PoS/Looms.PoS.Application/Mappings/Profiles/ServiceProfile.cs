using AutoMapper;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.Profiles;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<CreateServiceRequest, ServiceDao>(MemberList.Source);

        CreateMap<ServiceDao, ServiceResponse>();
    }
}
