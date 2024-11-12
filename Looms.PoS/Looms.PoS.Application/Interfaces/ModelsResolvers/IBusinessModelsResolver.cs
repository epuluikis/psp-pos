using Looms.PoS.Application.Models.Requests.Business;
using Looms.PoS.Application.Models.Responses.Business;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;

public interface IBusinessModelsResolver
{
    BusinessDao GetDaoFromRequest(CreateBusinessRequest createBusinessRequest);
    BusinessDao GetDaoFromDaoAndRequest(BusinessDao originalDao, UpdateBusinessRequest updateBusinessRequest);
    BusinessDao GetDeletedDao(BusinessDao originalDao);
    BusinessResponse GetResponseFromDao(BusinessDao businessDao);
    IEnumerable<BusinessResponse> GetResponseFromDao(IEnumerable<BusinessDao> businessDao);
}
