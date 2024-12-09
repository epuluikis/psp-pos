using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Order.Queries.GetOrder;

public record GetOrderQuery : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public GetOrderQuery(HttpRequest request, string orderId) : base(request)
    {
        Id = orderId;
    }
}