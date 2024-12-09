using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Order.Commands.CreateOrder;

public record CreateOrdersCommand : LoomsHttpRequest, IRequest<IActionResult>
{

    public CreateOrdersCommand(HttpRequest request) : base(request)
    {
    }
}