using Looms.PoS.Application.Features.PaymentTerminal.Commands.CreatePaymentTerminal;
using Looms.PoS.Application.Features.PaymentTerminal.Commands.DeletePaymentTerminal;
using Looms.PoS.Application.Features.PaymentTerminal.Commands.UpdatePaymentTerminal;
using Looms.PoS.Application.Features.PaymentTerminal.Queries.GetPaymentTerminal;
using Looms.PoS.Application.Features.PaymentTerminal.Queries.GetPaymentTerminals;
using Looms.PoS.Application.Models.Requests.PaymentTerminal;
using Looms.PoS.Application.Models.Responses.PaymentTerminal;
using Looms.PoS.Swagger.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Looms.PoS.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PaymentTerminalsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _contextAccessor;

    private const string EntityName = "paymentterminals";

    public PaymentTerminalsController(IMediator mediator, IHttpContextAccessor contextAccessor)
    {
        _mediator = mediator;
        _contextAccessor = contextAccessor;
    }

    [HttpPost($"/{EntityName}")]
    [SwaggerRequestType(typeof(CreatePaymentTerminalRequest))]
    [SwaggerResponse(StatusCodes.Status201Created, "PaymentTerminal successfully created.", typeof(PaymentTerminalResponse))]
    public async Task<IActionResult> CreatePaymentTerminal()
    {
        var comnand = new CreatePaymentTerminalCommand(GetRequest());

        return await _mediator.Send(comnand);
    }

    // TODO: scope to business
    [HttpGet($"/{EntityName}")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of paymentTerminals returned successfully.", typeof(List<PaymentTerminalResponse>))]
    public async Task<IActionResult> GetPaymentTerminals()
    {
        var query = new GetPaymentTerminalsQuery(GetRequest());

        return await _mediator.Send(query);
    }

    // TODO: scope to business
    [HttpGet($"/{EntityName}/{{paymentTerminalId}}")]
    [SwaggerResponse(StatusCodes.Status200OK, "PaymentTerminal details returned successfully.", typeof(PaymentTerminalResponse))]
    public async Task<IActionResult> GetPaymentTerminal(string paymentTerminalId)
    {
        var query = new GetPaymentTerminalQuery(GetRequest(), paymentTerminalId);

        return await _mediator.Send(query);
    }

    [HttpPut($"/{EntityName}/{{paymentTerminalId}}")]
    [SwaggerRequestType(typeof(UpdatePaymentTerminalRequest))]
    [SwaggerResponse(StatusCodes.Status200OK, "PaymentTerminal successfully updated.", typeof(PaymentTerminalResponse))]
    public async Task<IActionResult> UpdatePaymentTerminal(string paymentTerminalId)
    {
        var query = new UpdatePaymentTerminalCommand(GetRequest(), paymentTerminalId);

        return await _mediator.Send(query);
    }

    [HttpDelete($"/{EntityName}/{{paymentTerminalId}}")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "PaymentTerminal successfully deleted.")]
    public async Task<IActionResult> DeletePaymentTerminal(string paymentTerminalId)
    {
        var query = new DeletePaymentTerminalCommand(GetRequest(), paymentTerminalId);

        return await _mediator.Send(query);
    }

    private HttpRequest GetRequest()
    {
        return _contextAccessor.HttpContext!.Request;
    }
}
