using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.PaymentTerminal.Queries.GetPaymentTerminals;

public record GetPaymentTerminalsQuery : LoomsHttpRequest, IRequest<IActionResult>
{
    public GetPaymentTerminalsQuery(HttpRequest request) : base(request)
    {
    }
}
