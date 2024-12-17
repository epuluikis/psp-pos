using Looms.PoS.Application.Helpers;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.GiftCard.Queries.GetGiftCard;

public class GetGiftCardQueryHandler : IRequestHandler<GetGiftCardQuery, IActionResult>
{
    private readonly IGiftCardsRepository _giftCardsRepository;
    private readonly IGiftCardModelsResolver _giftCardModelsResolver;

    public GetGiftCardQueryHandler(
        IGiftCardsRepository giftCardsRepository,
        IGiftCardModelsResolver giftCardModelsResolver
    )
    {
        _giftCardsRepository = giftCardsRepository;
        _giftCardModelsResolver = giftCardModelsResolver;
    }

    public async Task<IActionResult> Handle(GetGiftCardQuery query, CancellationToken cancellationToken)
    {
        var giftCardDao = await _giftCardsRepository.GetAsyncByIdAndBusinessId(
            Guid.Parse(query.Id),
            Guid.Parse(HttpContextHelper.GetHeaderBusinessId(query.Request))
        );

        var response = _giftCardModelsResolver.GetResponseFromDao(giftCardDao);

        return new OkObjectResult(response);
    }
}
