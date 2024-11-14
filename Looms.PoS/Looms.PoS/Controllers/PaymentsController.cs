using Looms.PoS.Application.Features.Payment.Commands.CreatePayment;
using Looms.PoS.Application.Features.Payment.Queries.GetPayment;
using Looms.PoS.Application.Features.Payment.Queries.GetPayments;
using Looms.PoS.Application.Models.Requests.Payment;
using Looms.PoS.Application.Models.Responses.Payment;
using Looms.PoS.Swagger.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Looms.PoS.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _contextAccessor;

    private const string EntityName = "payments";

    public PaymentsController(IMediator mediator, IHttpContextAccessor contextAccessor)
    {
        _mediator = mediator;
        _contextAccessor = contextAccessor;
    }

    [HttpPost($"/{EntityName}")]
    [SwaggerRequestType(typeof(CreatePaymentRequest))]
    [SwaggerResponse(StatusCodes.Status201Created, "Payment successfully created.", typeof(List<PaymentResponse>))]
    public async Task<IActionResult> CreatePayment()
    {
        var comnand = new CreatePaymentCommand(GetRequest());

        return await _mediator.Send(comnand);
    }

    // TODO: scope to business
    [HttpGet($"/{EntityName}")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of payments returned successfully.", typeof(List<PaymentResponse>))]
    public async Task<IActionResult> GetPayments()
    {
        var query = new GetPaymentsQuery(GetRequest());

        return await _mediator.Send(query);
    }

    // TODO: scope to business
    [HttpGet($"/{EntityName}/{{paymentId}}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Payment details returned successfully.", typeof(PaymentResponse))]
    public async Task<IActionResult> GetPayment(string paymentId)
    {
        var query = new GetPaymentQuery(GetRequest(), paymentId);

        return await _mediator.Send(query);
    }

    private HttpRequest GetRequest()
    {
        return _contextAccessor.HttpContext!.Request;
    }
}
