using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.GiftCard.Commands.DeleteGiftCard;

public class DeleteGiftCardCommandHandler : IRequestHandler<DeleteGiftCardCommand, IActionResult>
{
    private readonly IGiftCardsRepository _giftCardsRepository;
    private readonly IGiftCardModelsResolver _giftCardModelsResolver;

    public DeleteGiftCardCommandHandler(
        IGiftCardsRepository giftCardsRepository,
        IGiftCardModelsResolver giftCardModelsResolver
    )
    {
        _giftCardsRepository = giftCardsRepository;
        _giftCardModelsResolver = giftCardModelsResolver;
    }

    public async Task<IActionResult> Handle(DeleteGiftCardCommand command, CancellationToken cancellationToken)
    {
        var originalDao = await _giftCardsRepository.GetAsync(Guid.Parse(command.Id));

        var giftCardDao = _giftCardModelsResolver.GetDeletedDao(originalDao);
        _ = await _giftCardsRepository.UpdateAsync(giftCardDao);

        return new NoContentResult();
    }
}
