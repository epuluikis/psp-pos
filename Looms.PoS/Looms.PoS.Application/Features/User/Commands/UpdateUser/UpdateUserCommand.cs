using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.User.Commands.UpdateUser;

public record UpdateUserCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public UpdateUserCommand(HttpRequest request, string userId) : base(request)
    {
        Id = userId;
    }
}
