using Looms.PoS.Application.Models.Requests.Tax;
using Looms.PoS.Application.Models.Responses.Tax;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;

public interface ITaxModelsResolver
{
    TaxDao GetDaoFromRequest(CreateTaxRequest createTaxRequest);
    TaxDao GetDaoFromDaoAndRequest(TaxDao originalDao, UpdateTaxRequest updateTaxRequest);
    TaxDao GetDeletedDao(TaxDao originalDao);
    TaxResponse GetResponseFromDao(TaxDao TaxDao);
    IEnumerable<TaxResponse> GetResponseFromDao(IEnumerable<TaxDao> TaxDao);
}