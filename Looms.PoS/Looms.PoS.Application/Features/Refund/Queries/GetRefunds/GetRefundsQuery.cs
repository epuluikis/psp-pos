using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Refund.Queries.GetRefunds;

public record GetRefundsQuery : LoomsHttpRequest, IRequest<IActionResult>
{
    public GetRefundsQuery(HttpRequest request) : base(request)
    {
    }
}
