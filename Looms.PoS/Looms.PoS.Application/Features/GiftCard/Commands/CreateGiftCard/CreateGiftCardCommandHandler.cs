using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.GiftCard;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.GiftCard.Commands.CreateGiftCard;

public class CreateGiftCardCommandHandler : IRequestHandler<CreateGiftCardCommand, IActionResult>
{
    private readonly IGiftCardsRepository _giftCardsRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IGiftCardModelsResolver _modelsResolver;

    public CreateGiftCardCommandHandler(
        IGiftCardsRepository giftCardsRepository,
        IHttpContentResolver httpContentResolver,
        IGiftCardModelsResolver modelsResolver)
    {
        _giftCardsRepository = giftCardsRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(CreateGiftCardCommand command, CancellationToken cancellationToken)
    {
        var giftCardRequest = await _httpContentResolver.GetPayloadAsync<CreateGiftCardRequest>(command.Request);

        // TODO: add businessId
        var giftCardDao = _modelsResolver.GetDaoFromRequest(giftCardRequest);
        var createdGiftCardDao = await _giftCardsRepository.CreateAsync(giftCardDao);

        var response = _modelsResolver.GetResponseFromDao(createdGiftCardDao);

        return new CreatedAtRouteResult($"/giftcards/{giftCardDao.Id}", response);
    }
}
