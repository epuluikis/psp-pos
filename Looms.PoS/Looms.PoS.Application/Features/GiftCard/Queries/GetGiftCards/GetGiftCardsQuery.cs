using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.GiftCard.Queries.GetGiftCards;

public record GetGiftCardsQuery : LoomsHttpRequest, IRequest<IActionResult>
{
    public GetGiftCardsQuery(HttpRequest request) : base(request)
    {
    }
}
