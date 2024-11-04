using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;

public interface IBusinessModelsResolver
{
    BusinessDao GetDaoFromRequest(CreateBusinessRequest createBusinessRequest);
    BusinessResponse GetResponseFromDao(BusinessDao businessDao);
    IEnumerable<BusinessResponse> GetResponseFromDao(IEnumerable<BusinessDao> businessDao);
}
