using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.GiftCard;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.GiftCard.Commands.UpdateGiftCard;

public class UpdateGiftCardCommandHandler : IRequestHandler<UpdateGiftCardCommand, IActionResult>
{
    private readonly IGiftCardsRepository _giftCardsRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IGiftCardModelsResolver _giftCardModelsResolver;

    public UpdateGiftCardCommandHandler(
        IGiftCardsRepository giftCardsRepository,
        IHttpContentResolver httpContentResolver,
        IGiftCardModelsResolver giftCardModelsResolver)
    {
        _giftCardsRepository = giftCardsRepository;
        _httpContentResolver = httpContentResolver;
        _giftCardModelsResolver = giftCardModelsResolver;
    }

    public async Task<IActionResult> Handle(UpdateGiftCardCommand command, CancellationToken cancellationToken)
    {
        var updateGiftCardRequest = await _httpContentResolver.GetPayloadAsync<UpdateGiftCardRequest>(command.Request);

        var originalDao = await _giftCardsRepository.GetAsync(Guid.Parse(command.Id));

        var giftCardDao = _giftCardModelsResolver.GetDaoFromDaoAndRequest(originalDao, updateGiftCardRequest);
        var updatedGiftCardDao = await _giftCardsRepository.UpdateAsync(giftCardDao);

        var response = _giftCardModelsResolver.GetResponseFromDao(updatedGiftCardDao);

        return new OkObjectResult(response);
    }
}
