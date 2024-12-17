using AutoMapper;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Tax;
using Looms.PoS.Application.Models.Responses.Tax;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.ModelsResolvers;

public class TaxModelsResolver : ITaxModelsResolver
{
    private readonly IMapper _mapper;

    public TaxModelsResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public TaxDao GetDaoFromRequest(CreateTaxRequest createTaxRequest)
    {
        return _mapper.Map<TaxDao>(createTaxRequest);
    }

    public TaxDao GetDaoFromRequestAndBusinessId(CreateTaxRequest createTaxRequest, Guid businessId)
    {
        return _mapper.Map<TaxDao>(createTaxRequest) with { BusinessId = businessId };
    }

    public TaxDao GetDaoFromDaoAndRequest(TaxDao originalDao, UpdateTaxRequest updateTaxRequest)
    {
        return _mapper.Map<TaxDao>(updateTaxRequest) with { Id = originalDao.Id, BusinessId = originalDao.BusinessId };
    }

    public TaxDao GetDeletedDao(TaxDao originalDao)
    {
        return originalDao with { IsDeleted = true };
    }

    public TaxResponse GetResponseFromDao(TaxDao taxDao)
    {
        return _mapper.Map<TaxResponse>(taxDao);
    }

    public IEnumerable<TaxResponse> GetResponseFromDao(IEnumerable<TaxDao> taxDao)
    {
        return _mapper.Map<IEnumerable<TaxResponse>>(taxDao);
    }
}
