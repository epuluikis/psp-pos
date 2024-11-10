using Looms.PoS.Application.Features.Refund.Commands.CreateRefund;
using Looms.PoS.Application.Features.Refund.Queries.GetRefund;
using Looms.PoS.Application.Features.Refund.Queries.GetRefunds;
using Looms.PoS.Application.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class RefundsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _contextAccessor;

    private const string EntityName = "refunds";

    public RefundsController(IMediator mediator, IHttpContextAccessor contextAccessor)
    {
        _mediator = mediator;
        _contextAccessor = contextAccessor;
    }

    [HttpPost($"/{EntityName}")]
    [ProducesResponseType<RefundResponse>(201)]
    public async Task<IActionResult> CreateRefund()
    {
        var command = new CreateRefundCommand(GetRequest());

        return await _mediator.Send(command);
    }

    [HttpGet($"/{EntityName}/{{refundId}}")]
    public async Task<IActionResult> GetRefund(string refundId)
    {
        var query = new GetRefundQuery(GetRequest(), refundId);

        return await _mediator.Send(query);
    }

    [HttpGet($"/{EntityName}")]
    public async Task<IActionResult> GetRefunds()
    {
        var query = new GetRefundsQuery();

        return await _mediator.Send(query);
    }

    private HttpRequest GetRequest()
    {
        return _contextAccessor.HttpContext!.Request;
    }

}