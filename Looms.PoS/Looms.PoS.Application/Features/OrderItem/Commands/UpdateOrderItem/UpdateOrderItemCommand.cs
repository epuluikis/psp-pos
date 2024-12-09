using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.OrderItem.Commands.UpdateOrderItem;

public record UpdateOrderItemCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public string OrderId { get; init; }
    public string Id { get; init; }

    public UpdateOrderItemCommand(HttpRequest request, string orderId, string id) : base(request)
    {
        OrderId = orderId;
        Id = id;
    }
}