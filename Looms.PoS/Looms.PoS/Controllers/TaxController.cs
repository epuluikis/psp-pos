using Looms.PoS.Application.Features.Tax.Commands.CreateTax;
using Looms.PoS.Application.Features.Tax.Commands.DeleteTax;
using Looms.PoS.Application.Features.Tax.Commands.UpdateTax;
using Looms.PoS.Application.Features.Tax.Queries.GetTax;
using Looms.PoS.Application.Features.Tax.Queries.GetTaxes;
using Looms.PoS.Application.Models.Requests.Tax;
using Looms.PoS.Application.Models.Responses.Tax;
using Looms.PoS.Swagger.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Looms.PoS.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TaxesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _contextAccessor;

    private const string EntityName = "taxes";

    public TaxesController(IMediator mediator, IHttpContextAccessor contextAccessor)
    {
        _mediator = mediator;
        _contextAccessor = contextAccessor;
    }

    [HttpPost($"/{EntityName}")]
    [SwaggerRequestType(typeof(CreateTaxRequest))]
    [SwaggerResponse(StatusCodes.Status201Created, "Tax successfully created.", typeof(TaxResponse))]
    public async Task<IActionResult> CreateTax()
    {
        var comnand = new CreateTaxCommand(GetRequest());

        return await _mediator.Send(comnand);
    }

    [HttpGet($"/{EntityName}")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of taxes returned successfully.", typeof(List<TaxResponse>))]
    public async Task<IActionResult> GetTaxes()
    {
        var query = new GetTaxesQuery(GetRequest());

        return await _mediator.Send(query);
    }

    [HttpGet($"/{EntityName}/{{taxId}}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Tax details returned successfully.", typeof(TaxResponse))]
    public async Task<IActionResult> GetTax(string taxId)
    {
        var query = new GetTaxQuery(GetRequest(), taxId);

        return await _mediator.Send(query);
    }

    [HttpPut($"/{EntityName}/{{taxId}}")]
    [SwaggerRequestType(typeof(UpdateTaxRequest))]
    [SwaggerResponse(StatusCodes.Status200OK, "Tax successfully updated.", typeof(TaxResponse))]
    public async Task<IActionResult> UpdateTax(string taxId)
    {
        var query = new UpdateTaxCommand(GetRequest(), taxId);

        return await _mediator.Send(query);
    }

    [HttpDelete($"/{EntityName}/{{taxId}}")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Tax successfully deleted.")]
    public async Task<IActionResult> DeleteTax(string taxId)
    {
        var query = new DeleteTaxCommand(GetRequest(), taxId);

        return await _mediator.Send(query);
    }

    private HttpRequest GetRequest()
    {
        return _contextAccessor.HttpContext!.Request;
    }
}
