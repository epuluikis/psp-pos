using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Business.Commands.DeleteBusiness;

public record DeleteBusinessCommand : GlobalLoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public DeleteBusinessCommand(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}