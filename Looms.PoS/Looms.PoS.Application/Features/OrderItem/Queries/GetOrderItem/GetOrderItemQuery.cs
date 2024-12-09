using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.OrderItem.Queries;

public record GetOrderItemQuery : LoomsHttpRequest, IRequest<IActionResult>
{
    public string OrderId { get; init; }
    public string Id { get; init; }
    
    public GetOrderItemQuery(HttpRequest request, string orderId, string id) : base(request)
    {
        OrderId = orderId;
        Id = id;
    }
}