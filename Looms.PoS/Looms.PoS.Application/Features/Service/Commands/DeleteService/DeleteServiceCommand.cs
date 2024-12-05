using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Service.Commands.DeleteService;

public record DeleteServiceCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public DeleteServiceCommand(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}