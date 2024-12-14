using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Product.Queries.GetProductVariationForProduct;

public record GetProductVariationForProductQuery : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public GetProductVariationForProductQuery(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}
