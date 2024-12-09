using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.GiftCard.Commands.CreateGiftCard;

public record CreateGiftCardCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public CreateGiftCardCommand(HttpRequest request) : base(request)
    {
    }
}
