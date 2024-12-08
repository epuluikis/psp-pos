using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Tax.Queries.GetTaxes;

public record GetTaxesQuery : LoomsHttpRequest, IRequest<IActionResult>
{
    public GetTaxesQuery(HttpRequest request) : base(request)
    {
    }
}
