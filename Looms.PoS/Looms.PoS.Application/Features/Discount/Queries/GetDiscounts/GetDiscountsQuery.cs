using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Discount.Queries;

public record GetDiscountsQuery : LoomsHttpRequest, IRequest<IActionResult>
{
    public GetDiscountsQuery(HttpRequest request) : base(request)
    {
    }
}
