using AutoMapper;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Requests.Discount;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Application.Models.Responses.Discount;
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
    {
        return _mapper.Map<DiscountDao>(createDiscountRequest);
    }

    public DiscountDao GetDaoFromRequestAndBusinessId(CreateDiscountRequest createDiscountRequest, Guid businessId)
    {   
        var category = createDiscountRequest.ProductId is not null ? DiscountTarget.Product : DiscountTarget.Order;

        return _mapper.Map<DiscountDao>(createDiscountRequest) with { 
            BusinessId = businessId,
            Target = category
        };
    }

    public DiscountDao GetDaoFromDaoAndRequest(DiscountDao discountDao, UpdateDiscountRequest updateDiscountRequest)
    {
        return _mapper.Map<DiscountDao>(updateDiscountRequest) with { Id = discountDao.Id, BusinessId = discountDao.BusinessId };
    }

    public DiscountResponse GetResponseFromDao(DiscountDao discountDao)
    {
        return _mapper.Map<DiscountResponse>(discountDao);
    }

    public IEnumerable<DiscountResponse> GetResponseFromDao(IEnumerable<DiscountDao> discountDao)
    {
        return _mapper.Map<IEnumerable<DiscountResponse>>(discountDao);
    }

    public DiscountDao GetDeletedDao(DiscountDao originalDao)
    {
        return originalDao with { IsDeleted = true };
    }
}
