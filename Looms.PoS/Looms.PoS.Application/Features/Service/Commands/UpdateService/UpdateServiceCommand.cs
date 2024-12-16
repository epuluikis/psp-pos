using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Service.Commands.UpdateService;

public record UpdateServiceCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public UpdateServiceCommand(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}