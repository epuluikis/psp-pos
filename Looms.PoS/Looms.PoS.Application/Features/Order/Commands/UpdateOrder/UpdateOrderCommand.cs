using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Order.Commands.UpdateOrder;

public record UpdateOrderCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public UpdateOrderCommand(HttpRequest request, string orderId) : base(request)
    {
        Id = orderId;
    }
}