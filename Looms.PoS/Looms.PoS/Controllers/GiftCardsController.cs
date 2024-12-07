using Looms.PoS.Application.Features.GiftCard.Commands.CreateGiftCard;
using Looms.PoS.Application.Features.GiftCard.Commands.DeleteGiftCard;
using Looms.PoS.Application.Features.GiftCard.Commands.UpdateGiftCard;
using Looms.PoS.Application.Features.GiftCard.Queries.GetGiftCard;
using Looms.PoS.Application.Features.GiftCard.Queries.GetGiftCards;
using Looms.PoS.Application.Models.Requests.GiftCard;
using Looms.PoS.Application.Models.Responses.GiftCard;
using Looms.PoS.Swagger.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Looms.PoS.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class GiftCardsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _contextAccessor;

    private const string EntityName = "giftCards";

    public GiftCardsController(IMediator mediator, IHttpContextAccessor contextAccessor)
    {
        _mediator = mediator;
        _contextAccessor = contextAccessor;
    }

    [HttpPost($"/{EntityName}")]
    [SwaggerRequestType(typeof(CreateGiftCardRequest))]
    [SwaggerResponse(StatusCodes.Status201Created, "GiftCard successfully created.", typeof(List<GiftCardResponse>))]
    public async Task<IActionResult> CreateGiftCard()
    {
        var comnand = new CreateGiftCardCommand(GetRequest());

        return await _mediator.Send(comnand);
    }

    // TODO: scope to business
    [HttpGet($"/{EntityName}")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of giftCards returned successfully.", typeof(List<GiftCardResponse>))]
    public async Task<IActionResult> GetGiftCards()
    {
        var query = new GetGiftCardsQuery(GetRequest());

        return await _mediator.Send(query);
    }

    // TODO: scope to business
    [HttpGet($"/{EntityName}/{{giftCardId}}")]
    [SwaggerResponse(StatusCodes.Status200OK, "GiftCard details returned successfully.", typeof(GiftCardResponse))]
    public async Task<IActionResult> GetGiftCard(string giftCardId)
    {
        var query = new GetGiftCardQuery(GetRequest(), giftCardId);

        return await _mediator.Send(query);
    }

    [HttpPut($"/{EntityName}/{{giftCardId}}")]
    [SwaggerResponse(StatusCodes.Status200OK, "GiftCard successfully updated.", typeof(GiftCardResponse))]
    public async Task<IActionResult> UpdateGiftCard(string giftCardId)
    {
        var query = new UpdateGiftCardCommand(GetRequest(), giftCardId);

        return await _mediator.Send(query);
    }

    [HttpDelete($"/{EntityName}/{{giftCardId}}")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "GiftCard successfully deleted.")]
    public async Task<IActionResult> DeleteGiftCard(string giftCardId)
    {
        var query = new DeleteGiftCardCommand(GetRequest(), giftCardId);

        return await _mediator.Send(query);
    }

    private HttpRequest GetRequest()
    {
        return _contextAccessor.HttpContext!.Request;
    }
}
