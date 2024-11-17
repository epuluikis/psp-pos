using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Refund.Commands.CreateRefund;

public record CreateRefundCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public CreateRefundCommand(HttpRequest request) : base(request)
    {
    }
}
