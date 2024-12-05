using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Service.Queries.GetService;

public record GetServiceQuery : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public GetServiceQuery(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}
