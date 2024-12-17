using Looms.PoS.Application.Helpers;
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
    private readonly IGiftCardModelsResolver _giftCardModelsResolver;

    public CreateGiftCardCommandHandler(
        IGiftCardsRepository giftCardsRepository,
        IHttpContentResolver httpContentResolver,
        IGiftCardModelsResolver giftCardModelsResolver
    )
    {
        _giftCardsRepository = giftCardsRepository;
        _httpContentResolver = httpContentResolver;
        _giftCardModelsResolver = giftCardModelsResolver;
    }

    public async Task<IActionResult> Handle(CreateGiftCardCommand command, CancellationToken cancellationToken)
    {
        var giftCardRequest = await _httpContentResolver.GetPayloadAsync<CreateGiftCardRequest>(command.Request);

        var giftCardDao = _giftCardModelsResolver.GetDaoFromRequest(
            giftCardRequest,
            Guid.Parse(HttpContextHelper.GetHeaderBusinessId(command.Request)),
            Guid.Parse(HttpContextHelper.GetUserId(command.Request))
        );

        giftCardDao = await _giftCardsRepository.CreateAsync(giftCardDao);

        var response = _giftCardModelsResolver.GetResponseFromDao(giftCardDao);

        return new CreatedAtRouteResult($"/giftcards/{giftCardDao.Id}", response);
    }
}
