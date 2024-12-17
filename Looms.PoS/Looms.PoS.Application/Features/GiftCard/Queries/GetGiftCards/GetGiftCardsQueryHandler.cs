using Looms.PoS.Application.Helpers;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.GiftCard.Queries.GetGiftCards;

public class GetGiftCardsQueryHandler : IRequestHandler<GetGiftCardsQuery, IActionResult>
{
    private readonly IGiftCardsRepository _giftCardsRepository;
    private readonly IGiftCardModelsResolver _giftCardsModelResolver;

    public GetGiftCardsQueryHandler(
        IGiftCardsRepository giftCardsRepository,
        IGiftCardModelsResolver giftCardsModelResolver
    )
    {
        _giftCardsRepository = giftCardsRepository;
        _giftCardsModelResolver = giftCardsModelResolver;
    }

    public async Task<IActionResult> Handle(GetGiftCardsQuery query, CancellationToken cancellationToken)
    {
        var giftCardDaos = await _giftCardsRepository.GetAllAsyncByBusinessId(
            Guid.Parse(HttpContextHelper.GetHeaderBusinessId(query.Request))
        );

        var response = _giftCardsModelResolver.GetResponseFromDao(giftCardDaos);

        return new OkObjectResult(response);
    }
}
