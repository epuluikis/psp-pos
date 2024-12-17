using Looms.PoS.Application.Features.Order.Commands.CreateOrder;
using Looms.PoS.Application.Features.Order.Commands.DeleteOrder;
using Looms.PoS.Application.Features.Order.Commands.UpdateOrder;
using Looms.PoS.Application.Features.Order.Queries.GetOrder;
using Looms.PoS.Application.Features.Order.Queries.GetOrders;
using Looms.PoS.Application.Models.Requests.Order;
using Looms.PoS.Application.Models.Responses.Order;
using Looms.PoS.Swagger.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Looms.PoS.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _contextAccessor;
    private const string EntityName = "orders";

    public OrdersController(IMediator mediator, IHttpContextAccessor contextAccessor)
    {
        _mediator = mediator;
        _contextAccessor = contextAccessor;
    }

    [HttpPost($"/{EntityName}")]
    [SwaggerRequestType(typeof(CreateOrderRequest))]
    [SwaggerResponse(StatusCodes.Status201Created, "Order created successfully.", typeof(OrderResponse))]
    public async Task<IActionResult> CreateOrder()
    {
        var command = new CreateOrdersCommand(GetRequest());

        return await _mediator.Send(command);
    }

    [HttpPut($"/{EntityName}/{{orderId}}")]
    [SwaggerRequestType(typeof(UpdateOrderRequest))]
    [SwaggerResponse(StatusCodes.Status200OK, "Order updated successfully.", typeof(OrderResponse))]
    public async Task<IActionResult> UpdateOrder(string orderId)
    {
        var command = new UpdateOrderCommand(GetRequest(), orderId);

        return await _mediator.Send(command);
    }

    [HttpDelete($"/{EntityName}/{{orderId}}")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Order deleted successfully.")]
    public async Task<IActionResult> DeleteOrder(string orderId)
    {
        var command = new DeleteOrderCommand(GetRequest(), orderId);

        return await _mediator.Send(command);
    }

    [HttpGet($"/{EntityName}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Orders retrieved successfully.", typeof(IEnumerable<OrderResponse>))]
    public async Task<IActionResult> GetOrders(string? status, string? userId)
    {
        var query = new GetOrdersQuery(GetRequest(), status, userId);

        return await _mediator.Send(query);
    }

    [HttpGet($"/{EntityName}/{{orderId}}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Order retrieved successfully.", typeof(OrderResponse))]
    public async Task<IActionResult> GetOrder(string orderId)
    {
        var query = new GetOrderQuery(GetRequest(), orderId);

        return await _mediator.Send(query);
    }

    private HttpRequest GetRequest()
    {
        return _contextAccessor.HttpContext!.Request;
    }
}
