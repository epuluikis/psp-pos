using Looms.PoS.Application.Features.PaymentTerminal.Commands.CreatePaymentTerminal;
using Looms.PoS.Application.Features.PaymentTerminal.Commands.DeletePaymentTerminal;
using Looms.PoS.Application.Features.PaymentTerminal.Commands.UpdatePaymentTerminal;
using Looms.PoS.Application.Features.PaymentTerminal.Queries.GetPaymentTerminal;
using Looms.PoS.Application.Features.PaymentTerminal.Queries.GetPaymentTerminals;
using Looms.PoS.Application.Features.PaymentWebhook.Commands.HandlePaymentWebhook;
using Looms.PoS.Application.Models.Requests.PaymentTerminal;
using Looms.PoS.Application.Models.Responses.PaymentTerminal;
using Looms.PoS.Swagger.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Looms.PoS.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PaymentWebhooksController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _contextAccessor;

    private const string EntityName = "paymentwebhooks";

    public PaymentWebhooksController(IMediator mediator, IHttpContextAccessor contextAccessor)
    {
        _mediator = mediator;
        _contextAccessor = contextAccessor;
    }

    [HttpPost($"/{EntityName}/{{paymentProviderId}}")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "PaymentWebhook successfully handled.")]
    public async Task<IActionResult> HandlePaymentWebhook(string paymentProviderId)
    {
        var comnand = new HandlePaymentWebhookCommand(GetRequest(), paymentProviderId);

        return await _mediator.Send(comnand);
    }

    private HttpRequest GetRequest()
    {
        return _contextAccessor.HttpContext!.Request;
    }
}
