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
        => _mapper.Map<TaxDao>(createTaxRequest);

    public TaxDao GetDaoFromDaoAndRequest(TaxDao originalDao, UpdateTaxRequest updateTaxRequest)
    {
        return _mapper.Map<TaxDao>(updateTaxRequest) with
        {
            Id = originalDao.Id
        };
    }

    public TaxDao GetDeletedDao(TaxDao originalDao)
    {
        return originalDao with
        {
            IsDeleted = true
        };
    }

    public TaxResponse GetResponseFromDao(TaxDao taxDao)
        => _mapper.Map<TaxResponse>(taxDao);

    public IEnumerable<TaxResponse> GetResponseFromDao(IEnumerable<TaxDao> taxDao)
        => _mapper.Map<IEnumerable<TaxResponse>>(taxDao);
}
