using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.OrderItem.Commands.CreateOrderItem;

public record CreateOrderItemsCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public string OrderId { get; init; }

    public CreateOrderItemsCommand(HttpRequest request, string orderId) : base(request)
    {
        OrderId = orderId;
    }
}
