using Looms.PoS.Application.Features.Discount.Commands.CreateDiscount;
using Looms.PoS.Application.Features.Discount.Commands.DeleteDiscount;
using Looms.PoS.Application.Features.Discount.Commands.UpdateDiscount;
using Looms.PoS.Application.Features.Discount.Queries;
using Looms.PoS.Application.Features.Discount.Queries.GetDiscount;
using Looms.PoS.Application.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    [ProducesResponseType<DiscountResponse>(201)]
    public async Task<IActionResult> CreateDiscount()
    {
        var command = new CreateDiscountsCommand(GetRequest());

        return await _mediator.Send(command);
    }

    [HttpGet($"/{EntityName}")]
    [ProducesResponseType<DiscountResponse>(200)]
    public async Task<IActionResult> GetDiscounts()
    {
        var query = new GetDiscountsQuery(GetRequest());

        return await _mediator.Send(query);
    }

    [HttpGet($"/{EntityName}/{{discountId}}")]
    [ProducesResponseType<DiscountResponse>(200)]
    public async Task<IActionResult> GetDiscount(string discountId)
    {
        var query = new GetDiscountQuery(GetRequest(), discountId);

        return await _mediator.Send(query);
    }

    [HttpPut($"/{EntityName}/{{discountId}}")]
    [ProducesResponseType<DiscountResponse>(200)]
    public async Task<IActionResult> UpdateDiscount(string discountId)
    {
        var command = new UpdateDiscountCommand(GetRequest(), discountId);

        return await _mediator.Send(command);
    }

    [HttpDelete($"/{EntityName}/{{discountId}}")]
    [ProducesResponseType(204)]
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
