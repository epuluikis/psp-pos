using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Business.Queries.GetBusiness;

public record GetBusinessQuery : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public GetBusinessQuery(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}
