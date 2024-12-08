using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Auth.Commands.Login;

public record LoginCommand : AuthRequest, IRequest<IActionResult>
{
    public LoginCommand(HttpRequest request) : base(request)
    {
    }
}
