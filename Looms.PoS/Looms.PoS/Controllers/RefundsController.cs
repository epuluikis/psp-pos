using Looms.PoS.Application.Features.Refund.Commands.CreateRefund;
using Looms.PoS.Application.Features.Refund.Queries.GetRefund;
using Looms.PoS.Application.Features.Refund.Queries.GetRefunds;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Requests.Refund;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Application.Models.Responses.Refund;
using Looms.PoS.Swagger.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerRequestType(typeof(CreateRefundRequest))]
    [SwaggerResponse(StatusCodes.Status201Created, "Refund successfully created.", typeof(RefundResponse))]
    public async Task<IActionResult> CreateRefund()
    {
        var command = new CreateRefundCommand(GetRequest());

        return await _mediator.Send(command);
    }

    [HttpGet($"/{EntityName}/{{refundId}}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Refund details returned successfuly.", typeof(RefundResponse))]
    public async Task<IActionResult> GetRefund(string refundId)
    {
        var query = new GetRefundQuery(GetRequest(), refundId);

        return await _mediator.Send(query);
    }

    [HttpGet($"/{EntityName}")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of refunds returned successfully.", typeof(List<RefundResponse>))]
    public async Task<IActionResult> GetRefunds()
    {
        var query = new GetRefundsQuery(GetRequest());

        return await _mediator.Send(query);
    }

    private HttpRequest GetRequest()
    {
        return _contextAccessor.HttpContext!.Request;
    }
}
