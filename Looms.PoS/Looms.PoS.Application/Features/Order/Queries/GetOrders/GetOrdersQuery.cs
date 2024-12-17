using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Order.Queries.GetOrders;

public record GetOrdersQuery : LoomsHttpRequest, IRequest<IActionResult>
{
    public string? Status { get; init; }
    public string? UserId { get; init; }

    public GetOrdersQuery(HttpRequest request, string? status, string? userId) : base(request)
    {
        Status = status;
        UserId = userId;
    }
}
