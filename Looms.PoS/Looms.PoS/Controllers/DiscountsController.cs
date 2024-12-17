using Looms.PoS.Application.Features.Discount.Commands.CreateDiscount;
using Looms.PoS.Application.Features.Discount.Commands.DeleteDiscount;
using Looms.PoS.Application.Features.Discount.Commands.UpdateDiscount;
using Looms.PoS.Application.Features.Discount.Queries.GetDiscount;
using Looms.PoS.Application.Features.Discount.Queries.GetDiscounts;
using Looms.PoS.Application.Models.Requests.Discount;
using Looms.PoS.Application.Models.Responses.Discount;
using Looms.PoS.Swagger.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Looms.PoS.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class DiscountsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _contextAccessor;

    private const string EntityName = "discounts";

    public DiscountsController(IMediator mediator, IHttpContextAccessor contextAccessor)
    {
        _mediator = mediator;
        _contextAccessor = contextAccessor;
    }

    [HttpPost($"/{EntityName}")]
    [SwaggerRequestType(typeof(CreateDiscountRequest))]
    [SwaggerResponse(StatusCodes.Status201Created, "Discount successfully created.", typeof(DiscountResponse))]
    public async Task<IActionResult> CreateDiscount()
    {
        var command = new CreateDiscountsCommand(GetRequest());

        return await _mediator.Send(command);
    }

    [HttpGet($"/{EntityName}")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of discounts returned successfully.", typeof(List<DiscountResponse>))]
    public async Task<IActionResult> GetDiscounts()
    {
        var query = new GetDiscountsQuery(GetRequest());

        return await _mediator.Send(query);
    }

    [HttpGet($"/{EntityName}/{{discountId}}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Discount details returned successfully.", typeof(DiscountResponse))]
    public async Task<IActionResult> GetDiscount(string discountId)
    {
        var query = new GetDiscountQuery(GetRequest(), discountId);

        return await _mediator.Send(query);
    }

    [HttpPut($"/{EntityName}/{{discountId}}")]
    [SwaggerRequestType(typeof(UpdateDiscountRequest))]
    [SwaggerResponse(StatusCodes.Status200OK, "Discount successfully updated.", typeof(DiscountResponse))]
    public async Task<IActionResult> UpdateDiscount(string discountId)
    {
        var command = new UpdateDiscountCommand(GetRequest(), discountId);

        return await _mediator.Send(command);
    }

    [HttpDelete($"/{EntityName}/{{discountId}}")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Discount successfully deleted.")]
    public async Task<IActionResult> DeleteDiscount(string discountId)
    {
        var query = new DeleteDiscountCommand(GetRequest(), discountId);
        return await _mediator.Send(query);
    }

    private HttpRequest GetRequest()
    {
        return _contextAccessor.HttpContext!.Request;
    }
}
