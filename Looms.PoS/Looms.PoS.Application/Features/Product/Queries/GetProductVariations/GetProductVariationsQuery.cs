using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Product.Queries.GetProductVariations;

public record GetProductVariationsQuery : LoomsHttpRequest, IRequest<IActionResult>
{
    public GetProductVariationsQuery(HttpRequest request) : base(request)
    {
    }
}
