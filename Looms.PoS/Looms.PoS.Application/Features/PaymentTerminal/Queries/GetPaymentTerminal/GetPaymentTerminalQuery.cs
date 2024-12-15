using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.PaymentTerminal.Queries.GetPaymentTerminal;

public record GetPaymentTerminalQuery : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public GetPaymentTerminalQuery(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}
