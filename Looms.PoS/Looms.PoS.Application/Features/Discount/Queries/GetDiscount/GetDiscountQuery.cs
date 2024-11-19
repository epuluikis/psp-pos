using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Discount.Queries.GetDiscount;

public record GetDiscountQuery : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public GetDiscountQuery(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}
