using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.GiftCard.Commands.DeleteGiftCard;

public class DeleteGiftCardCommandHandler : IRequestHandler<DeleteGiftCardCommand, IActionResult>
{
    private readonly IGiftCardsRepository _giftCardsRepository;
    private readonly IGiftCardModelsResolver _modelsResolver;

    public DeleteGiftCardCommandHandler(
        IGiftCardsRepository giftCardsRepository,
        IGiftCardModelsResolver modelsResolver)
    {
        _giftCardsRepository = giftCardsRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(DeleteGiftCardCommand command, CancellationToken cancellationToken)
    {
        var originalDao = await _giftCardsRepository.GetAsync(Guid.Parse(command.Id));

        var giftCardDao = _modelsResolver.GetDeletedDao(originalDao);
        _ = await _giftCardsRepository.UpdateAsync(giftCardDao);

        return new NoContentResult();
    }
}
