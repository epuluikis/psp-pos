using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;

public interface IServiceModelsResolver
{
    ServiceDao GetDaoFromRequest(CreateServiceRequest createServiceRequest);
    ServiceResponse GetResponseFromDao(ServiceDao serviceDao);
    IEnumerable<ServiceResponse> GetResponseFromDao(IEnumerable<ServiceDao> serviceDao);
}
