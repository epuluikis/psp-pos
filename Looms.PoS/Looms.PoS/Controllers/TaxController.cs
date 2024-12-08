using Looms.PoS.Application.Features.Tax.Commands.CreateTax;
using Looms.PoS.Application.Features.Tax.Commands.DeleteTax;
using Looms.PoS.Application.Features.Tax.Commands.UpdateTax;
using Looms.PoS.Application.Features.Tax.Queries.GetTax;
using Looms.PoS.Application.Features.Tax.Queries.GetTaxes;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> CreateTax()
    {
        var comnand = new CreateTaxCommand(GetRequest());

        return await _mediator.Send(comnand);
    }

    [HttpGet($"/{EntityName}")]
    public async Task<IActionResult> GetTaxes()
    {
        var query = new GetTaxesQuery(GetRequest());

        return await _mediator.Send(query);
    }

    [HttpGet($"/{EntityName}/{{taxId}}")]
    public async Task<IActionResult> GetTax(string taxId)
    {
        var query = new GetTaxQuery(GetRequest(), taxId);

        return await _mediator.Send(query);
    }

    [HttpPut($"/{EntityName}/{{taxId}}")]
    public async Task<IActionResult> UpdateTax(string taxId)
    {
        var query = new UpdateTaxCommand(GetRequest(), taxId);

        return await _mediator.Send(query);
    }

    [HttpDelete($"/{EntityName}/{{taxId}}")]
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
