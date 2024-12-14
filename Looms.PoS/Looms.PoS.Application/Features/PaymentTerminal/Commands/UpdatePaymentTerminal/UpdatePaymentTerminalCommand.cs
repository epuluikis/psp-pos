using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.PaymentTerminal.Commands.UpdatePaymentTerminal;

public record UpdatePaymentTerminalCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public UpdatePaymentTerminalCommand(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}
