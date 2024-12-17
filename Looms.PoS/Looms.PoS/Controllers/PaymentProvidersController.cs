using Looms.PoS.Application.Features.PaymentProvider.Commands.CreatePaymentProvider;
using Looms.PoS.Application.Features.PaymentProvider.Commands.DeletePaymentProvider;
using Looms.PoS.Application.Features.PaymentProvider.Commands.UpdatePaymentProvider;
using Looms.PoS.Application.Features.PaymentProvider.Queries.GetPaymentProvider;
using Looms.PoS.Application.Features.PaymentProvider.Queries.GetPaymentProviders;
using Looms.PoS.Application.Models.Requests.PaymentProvider;
using Looms.PoS.Application.Models.Responses.PaymentProvider;
using Looms.PoS.Swagger.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Looms.PoS.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PaymentProvidersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _contextAccessor;

    private const string EntityName = "paymentproviders";

    public PaymentProvidersController(IMediator mediator, IHttpContextAccessor contextAccessor)
    {
        _mediator = mediator;
        _contextAccessor = contextAccessor;
    }

    [HttpPost($"/{EntityName}")]
    [SwaggerRequestType(typeof(CreatePaymentProviderRequest))]
    [SwaggerResponse(StatusCodes.Status201Created, "PaymentProvider successfully created.", typeof(PaymentProviderResponse))]
    public async Task<IActionResult> CreatePaymentProvider()
    {
        var comnand = new CreatePaymentProviderCommand(GetRequest());

        return await _mediator.Send(comnand);
    }

    [HttpGet($"/{EntityName}")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of paymentProviders returned successfully.", typeof(List<PaymentProviderResponse>))]
    public async Task<IActionResult> GetPaymentProviders()
    {
        var query = new GetPaymentProvidersQuery(GetRequest());

        return await _mediator.Send(query);
    }

    [HttpGet($"/{EntityName}/{{paymentProviderId}}")]
    [SwaggerResponse(StatusCodes.Status200OK, "PaymentProvider details returned successfully.", typeof(PaymentProviderResponse))]
    public async Task<IActionResult> GetPaymentProvider(string paymentProviderId)
    {
        var query = new GetPaymentProviderQuery(GetRequest(), paymentProviderId);

        return await _mediator.Send(query);
    }

    [HttpPut($"/{EntityName}/{{paymentProviderId}}")]
    [SwaggerRequestType(typeof(UpdatePaymentProviderRequest))]
    [SwaggerResponse(StatusCodes.Status200OK, "PaymentProvider successfully updated.", typeof(PaymentProviderResponse))]
    public async Task<IActionResult> UpdatePaymentProvider(string paymentProviderId)
    {
        var query = new UpdatePaymentProviderCommand(GetRequest(), paymentProviderId);

        return await _mediator.Send(query);
    }

    [HttpDelete($"/{EntityName}/{{paymentProviderId}}")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "PaymentProvider successfully deleted.")]
    public async Task<IActionResult> DeletePaymentProvider(string paymentProviderId)
    {
        var query = new DeletePaymentProviderCommand(GetRequest(), paymentProviderId);

        return await _mediator.Send(query);
    }

    private HttpRequest GetRequest()
    {
        return _contextAccessor.HttpContext!.Request;
    }
}
