using Looms.PoS.Application.Models.Requests.Service;
using Looms.PoS.Application.Models.Responses.Service;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;

public interface IServiceModelsResolver
{
    ServiceDao GetDaoFromRequest(CreateServiceRequest createServiceRequest);
    ServiceDao GetDaoFromRequest(CreateServiceRequest createServiceRequest, Guid businessId);
    ServiceDao GetDaoFromRequest(CreateServiceRequest createServiceRequest, Guid businessId, TaxDao tax);
    ServiceDao GetDaoFromDaoAndRequest(ServiceDao originalDao, UpdateServiceRequest updateServiceRequest);
    ServiceDao GetDeletedDao(ServiceDao originalDao);
    ServiceResponse GetResponseFromDao(ServiceDao serviceDao);
    IEnumerable<ServiceResponse> GetResponseFromDao(IEnumerable<ServiceDao> serviceDao);
}
