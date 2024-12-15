using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Tax.Queries.GetTax;

public record GetTaxQuery : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public GetTaxQuery(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}
