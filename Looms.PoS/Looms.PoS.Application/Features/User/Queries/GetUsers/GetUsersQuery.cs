using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.User.Queries.GetUsers;

public record GetUsersQuery : LoomsHttpRequest, IRequest<IActionResult>
{
    public GetUsersQuery(HttpRequest request) : base(request)
    {
    }
}
