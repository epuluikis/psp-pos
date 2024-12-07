using AutoMapper;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.GiftCard;
using Looms.PoS.Application.Models.Responses.GiftCard;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.ModelsResolvers;

public class GiftCardModelsResolver : IGiftCardModelsResolver
{
    private readonly IMapper _mapper;

    public GiftCardModelsResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public GiftCardDao GetDaoFromRequest(CreateGiftCardRequest createGiftCardRequest)
    {
        return _mapper.Map<GiftCardDao>(createGiftCardRequest);
    }

    public GiftCardDao GetDaoFromDaoAndRequest(GiftCardDao originalDao, UpdateGiftCardRequest updateGiftCardRequest)
    {
        return _mapper.Map<GiftCardDao>(updateGiftCardRequest) with
        {
            Id = originalDao.Id,
            InitialBalance = originalDao.InitialBalance,
            IssuedBy = originalDao.IssuedBy,
            BusinessId = originalDao.BusinessId,
            IsDeleted = originalDao.IsDeleted
        };
    }

    public GiftCardDao GetDaoFromDaoAndCurrentBalance(GiftCardDao originalDao, decimal currentBalance)
    {
        return _mapper.Map<GiftCardDao>(originalDao) with { CurrentBalance = currentBalance };
    }

    public GiftCardDao GetDeletedDao(GiftCardDao originalDao)
    {
        return originalDao with { IsDeleted = true };
    }

    public GiftCardResponse GetResponseFromDao(GiftCardDao giftCardDao)
    {
        return _mapper.Map<GiftCardResponse>(giftCardDao);
    }

    public IEnumerable<GiftCardResponse> GetResponseFromDao(IEnumerable<GiftCardDao> giftCardDao)
    {
        return _mapper.Map<IEnumerable<GiftCardResponse>>(giftCardDao);
    }
}
