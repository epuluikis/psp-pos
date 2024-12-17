using Looms.PoS.Application.Models.Requests.GiftCard;
using Looms.PoS.Application.Models.Responses.GiftCard;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;

public interface IGiftCardModelsResolver
{
    GiftCardDao GetDaoFromRequest(CreateGiftCardRequest createGiftCardRequest, Guid businessId, Guid issuedById);
    GiftCardDao GetDaoFromDaoAndRequest(GiftCardDao originalDao, UpdateGiftCardRequest updateGiftCardRequest);
    GiftCardDao GetDaoFromDaoAndCurrentBalance(GiftCardDao originalDao, decimal currentBalance);
    GiftCardDao GetDeletedDao(GiftCardDao originalDao);
    GiftCardResponse GetResponseFromDao(GiftCardDao giftCardDao);
    IEnumerable<GiftCardResponse> GetResponseFromDao(IEnumerable<GiftCardDao> giftCardDao);
}
