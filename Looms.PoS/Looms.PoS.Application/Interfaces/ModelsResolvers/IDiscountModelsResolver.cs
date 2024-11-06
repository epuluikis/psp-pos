using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;
public interface IDiscountModelsResolver
{
    DiscountDao GetDaoFromRequest(CreateDiscountRequest createDiscountRequest);
    DiscountResponse GetResponseFromDao(DiscountDao discountDao);
    IEnumerable<DiscountResponse> GetResponseFromDao(IEnumerable<DiscountDao> discountDao);
}
