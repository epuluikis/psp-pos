using Looms.PoS.Application.Models.Requests.Service;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;

public interface IServiceModelsResolver
{
    ServiceDao GetDaoFromRequest(CreateServiceRequest createServiceRequest);
    ServiceDao GetDaoFromDaoAndRequest(ServiceDao originalDao, UpdateServiceRequest updateServiceRequest);
    ServiceDao GetDeletedDao(ServiceDao originalDao);
    ServiceResponse GetResponseFromDao(ServiceDao serviceDao);
    IEnumerable<ServiceResponse> GetResponseFromDao(IEnumerable<ServiceDao> serviceDao);
}
