using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.PaymentTerminal.Commands.DeletePaymentTerminal;

public record DeletePaymentTerminalCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public DeletePaymentTerminalCommand(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}
