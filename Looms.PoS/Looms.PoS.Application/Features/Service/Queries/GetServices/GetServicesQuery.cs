using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Service.Queries.GetServices;

public record GetServicesQuery : LoomsHttpRequest, IRequest<IActionResult>
{
    public GetServicesQuery(HttpRequest request) : base(request)
    {
    }
}
