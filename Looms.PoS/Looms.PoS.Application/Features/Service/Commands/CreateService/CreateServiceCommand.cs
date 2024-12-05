using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Service.Commands.CreateService;

public record CreateServiceCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public CreateServiceCommand(HttpRequest request) : base(request)
    {
    }
}
