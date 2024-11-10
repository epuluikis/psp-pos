using AutoMapper;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.ModelsResolvers;

public class BusinessModelsResolver : IBusinessModelsResolver
{
    private readonly IMapper _mapper;

    public BusinessModelsResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public BusinessDao GetDaoFromRequest(CreateBusinessRequest createBusinessRequest)
        => _mapper.Map<BusinessDao>(createBusinessRequest);

    public BusinessResponse GetResponseFromDao(BusinessDao businessDao)
        => _mapper.Map<BusinessResponse>(businessDao);

    public IEnumerable<BusinessResponse> GetResponseFromDao(IEnumerable<BusinessDao> businessDao)
        => _mapper.Map<IEnumerable<BusinessResponse>>(businessDao);
}
