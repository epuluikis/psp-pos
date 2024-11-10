using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Payment.Commands.CreatePayment;

public record CreatePaymentCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public CreatePaymentCommand(HttpRequest request) : base(request)
    {
    }
}
