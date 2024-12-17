using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Order.Commands.DeleteOrder;

public record DeleteOrderCommand : LoomsHttpRequest, IRequest<IActionResult>
{
    public string Id { get; init; }

    public DeleteOrderCommand(HttpRequest request, string id) : base(request)
    {
        Id = id;
    }
}
