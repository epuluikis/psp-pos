using Looms.PoS.Application.Features.Discount.Commands.CreateDiscount;
using Looms.PoS.Application.Features.OrderItem.Commands.DeleteOrderItem;
using Looms.PoS.Application.Features.OrderItem.Commands.UpdateOrderItem;
using Looms.PoS.Application.Features.OrderItem.Queries;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Swagger.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Looms.PoS.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class OrderItemsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _contextAccessor;

    private const string EntityName = "items";

    public OrderItemsController(IMediator mediator, IHttpContextAccessor contextAccessor)
    {
        _mediator = mediator;
        _contextAccessor = contextAccessor;
    }

    [HttpPost($"/orders/{{orderId}}/{EntityName}")]
    [SwaggerRequestType(typeof(CreateOrderItemRequest))]
    [SwaggerResponse(StatusCodes.Status201Created, "Order item created successfully.", typeof(OrderResponse))]
    public async Task<IActionResult> CreateOrderItem(string orderId)
    {
        var command = new CreateOrderItemsCommand(GetRequest(), orderId);

        return await _mediator.Send(command);
    }

    [HttpPut($"/orders/{{orderId}}/{EntityName}/{{orderItemId}}")]
    [SwaggerRequestType(typeof(UpdateOrderItemRequest))]
    [SwaggerResponse(StatusCodes.Status200OK, "Order item updated successfully.", typeof(OrderItemResponse))]
    public async Task<IActionResult> UpdateOrderItem(string orderId, string orderItemId)
    {
        var command = new UpdateOrderItemCommand(GetRequest(), orderId, orderItemId);

        return await _mediator.Send(command);
    }

    [HttpDelete($"/orders/{{orderId}}/{EntityName}/{{orderItemId}}")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Order item deleted successfully.")]
    public async Task<IActionResult> DeleteOrderItem(string orderId, string orderItemId)
    {
        var command = new DeleteOrderItemCommand(GetRequest(), orderId, orderItemId);

        return await _mediator.Send(command);
    }

    [HttpGet($"/orders/{{orderId}}/{EntityName}/{{orderItemId}}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Order item retrieved successfully.", typeof(OrderItemResponse))]
    public async Task<IActionResult> GetOrderItem(string orderId, string orderItemId)
    {
        var query = new GetOrderItemQuery(GetRequest(), orderId, orderItemId);

        return await _mediator.Send(query);
    }

    private HttpRequest GetRequest()
    {
        return _contextAccessor.HttpContext!.Request;
    }
}