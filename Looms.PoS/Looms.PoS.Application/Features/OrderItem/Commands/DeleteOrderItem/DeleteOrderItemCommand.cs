using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.OrderItem.Commands.DeleteOrderItem;

public record DeleteOrderItemCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public string OrderId { get; init; }
    public string Id { get; init; }

    public DeleteOrderItemCommand(HttpRequest request, string orderId, string id) : base(request)
    {
        OrderId = orderId;
        Id = id;
    }
}