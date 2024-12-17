using Looms.PoS.Application.Helpers;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.GiftCard.Queries.GetGiftCard;

public class GetGiftCardQueryHandler : IRequestHandler<GetGiftCardQuery, IActionResult>
{
    private readonly IGiftCardsRepository _giftCardsRepository;
    private readonly IGiftCardModelsResolver _modelsResolver;

    public GetGiftCardQueryHandler(IGiftCardsRepository giftCardsRepository, IGiftCardModelsResolver modelsResolver)
    {
        _giftCardsRepository = giftCardsRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetGiftCardQuery query, CancellationToken cancellationToken)
    {
        var giftCardDao = await _giftCardsRepository.GetAsyncByIdAndBusinessId(
            Guid.Parse(query.Id),
            Guid.Parse(HttpContextHelper.GetHeaderBusinessId(query.Request))
        );

        var response = _modelsResolver.GetResponseFromDao(giftCardDao);

        return new OkObjectResult(response);
    }
}
