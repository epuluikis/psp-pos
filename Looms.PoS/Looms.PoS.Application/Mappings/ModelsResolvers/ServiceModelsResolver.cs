using AutoMapper;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.ModelsResolvers;

public class ServiceModelsResolver : IServiceModelsResolver
{
    private readonly IMapper _mapper;

    public ServiceModelsResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public ServiceDao GetDaoFromRequest(CreateServiceRequest createServiceRequest)
        => _mapper.Map<ServiceDao>(createServiceRequest);

    public ServiceResponse GetResponseFromDao(ServiceDao serviceDao)
        => _mapper.Map<ServiceResponse>(serviceDao);

    public IEnumerable<ServiceResponse> GetResponseFromDao(IEnumerable<ServiceDao> serviceDao)
        => _mapper.Map<IEnumerable<ServiceResponse>>(serviceDao);
}
