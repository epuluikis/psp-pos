using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.GiftCard.Commands.UpdateGiftCard;

public record UpdateGiftCardCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public UpdateGiftCardCommand(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}
