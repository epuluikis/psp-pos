using AutoMapper;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.ModelsResolvers;
public class DiscountModelsResolver : IDiscountModelsResolver
{
    private readonly IMapper _mapper;
    public DiscountModelsResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public DiscountDao GetDaoFromRequest(CreateDiscountRequest createDiscountRequest)
        => _mapper.Map<DiscountDao>(createDiscountRequest);

    public DiscountDao GetDaoFromDaoAndRequest(DiscountDao discountDao, UpdateDiscountRequest updateDiscountRequest)
    {
        return _mapper.Map<DiscountDao>(updateDiscountRequest) with
        {
            Id = discountDao.Id
        };
    }

    public DiscountResponse GetResponseFromDao(DiscountDao discountDao)
        => _mapper.Map<DiscountResponse>(discountDao);

    public IEnumerable<DiscountResponse> GetResponseFromDao(IEnumerable<DiscountDao> discountDao)
        => _mapper.Map<IEnumerable<DiscountResponse>>(discountDao);

}
