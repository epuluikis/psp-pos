using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.PaymentTerminal.Commands.CreatePaymentTerminal;

public record CreatePaymentTerminalCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public CreatePaymentTerminalCommand(HttpRequest request) : base(request)
    {
    }
}
