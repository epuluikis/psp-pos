using AutoMapper;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Service;
using Looms.PoS.Application.Models.Responses.Service;
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
    {
        return _mapper.Map<ServiceDao>(createServiceRequest);
    }

    public ServiceDao GetDaoFromRequest(CreateServiceRequest createServiceRequest, Guid businessId)
    {
        return _mapper.Map<ServiceDao>(createServiceRequest) with { BusinessId = businessId };
    }

    public ServiceDao GetDaoFromRequest(CreateServiceRequest createServiceRequest, Guid businessId, TaxDao tax)
    {
        return _mapper.Map<ServiceDao>(createServiceRequest) with { BusinessId = businessId, Tax = tax, TaxId = tax.Id };
    }

    public ServiceDao GetDaoFromDaoAndRequest(ServiceDao originalDao, UpdateServiceRequest updateServiceRequest)
    {
        return _mapper.Map<ServiceDao>(updateServiceRequest) with { Id = originalDao.Id };
    }

    public ServiceDao GetDeletedDao(ServiceDao originalDao)
    {
        return originalDao with { IsDeleted = true };
    }

    public ServiceResponse GetResponseFromDao(ServiceDao serviceDao)
    {
        return _mapper.Map<ServiceResponse>(serviceDao);
    }

    public IEnumerable<ServiceResponse> GetResponseFromDao(IEnumerable<ServiceDao> serviceDao)
    {
        return _mapper.Map<IEnumerable<ServiceResponse>>(serviceDao);
    }
}
