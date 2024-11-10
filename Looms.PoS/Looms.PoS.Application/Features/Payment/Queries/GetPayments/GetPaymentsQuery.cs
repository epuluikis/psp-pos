using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Payment.Queries.GetPayments;

public record GetPaymentsQuery : LoomsHttpRequest, IRequest<IActionResult>
{
    public GetPaymentsQuery(HttpRequest request) : base(request)
    {
    }
}
