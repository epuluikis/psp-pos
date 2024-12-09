﻿using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.GiftCard.Queries.GetGiftCards;

public class GetGiftCardsQueryHandler : IRequestHandler<GetGiftCardsQuery, IActionResult>
{
    private readonly IGiftCardsRepository _giftCardsRepository;
    private readonly IGiftCardModelsResolver _modelsResolver;

    public GetGiftCardsQueryHandler(IGiftCardsRepository giftCardsRepository, IGiftCardModelsResolver modelsResolver)
    {
        _giftCardsRepository = giftCardsRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetGiftCardsQuery request, CancellationToken cancellationToken)
    {
        var giftCardDaos = await _giftCardsRepository.GetAllAsync();

        var response = _modelsResolver.GetResponseFromDao(giftCardDaos);

        return new OkObjectResult(response);
    }
}
