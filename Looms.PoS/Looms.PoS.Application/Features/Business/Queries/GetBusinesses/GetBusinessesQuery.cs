using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Business.Queries.GetBusinesses;

public record GetBusinessesQuery : GlobalLoomsHttpRequest, IRequest<IActionResult>
{
    public GetBusinessesQuery(HttpRequest request) : base(request)
    {
    }
}
