using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Product.Queries.GetProducts;

public record GetProductsQuery : LoomsHttpRequest, IRequest<IActionResult>
{
    public GetProductsQuery(HttpRequest request) : base(request)
    {
    }
}
