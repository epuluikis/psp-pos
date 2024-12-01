using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.User.Commands.CreateUser;

public record CreateUserCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public CreateUserCommand(HttpRequest request) : base(request)
    {
    }
}
