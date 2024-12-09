﻿using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.GiftCard.Commands.DeleteGiftCard;

public record DeleteGiftCardCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public DeleteGiftCardCommand(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}
