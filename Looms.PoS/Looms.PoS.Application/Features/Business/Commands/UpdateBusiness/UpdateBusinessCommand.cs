using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Business.Commands.UpdateBusiness;

public record UpdateBusinessCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public UpdateBusinessCommand(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}
