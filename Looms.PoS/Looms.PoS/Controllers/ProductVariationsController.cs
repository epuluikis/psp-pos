using Looms.PoS.Application.Features.Product.Commands.CreateProductVariation;
using Looms.PoS.Application.Features.Product.Commands.DeleteProductVariation;
using Looms.PoS.Application.Features.Product.Commands.UpdateProductVariation;
using Looms.PoS.Application.Features.Product.Queries.GetProductVariation;
using Looms.PoS.Application.Features.Product.Queries.GetProductVariations;
using Looms.PoS.Application.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ProductVariationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _contextAccessor;

    private const string EntityName = "productvariation";

    public ProductVariationsController(IMediator mediator, IHttpContextAccessor contextAccessor)
    {
        _mediator = mediator;
        _contextAccessor = contextAccessor;
    }

    [HttpPost($"/{EntityName}")]
    public async Task<IActionResult> CreateProductVariation()
    {
        var command = new CreateProductVariationCommand(GetRequest());

        return await _mediator.Send(command);
    }

    [HttpGet($"/{EntityName}")]
    public async Task<IActionResult> GetProductVariations()
    {
        var query = new GetProductVariationsQuery(GetRequest());

        return await _mediator.Send(query);
    }

    [HttpGet($"/{EntityName}/{{productVariationId}}")]
    public async Task<IActionResult> GetProductVariations(string productVariationId)
    {
        var query = new GetProductVariationQuery(GetRequest(), productVariationId);

        return await _mediator.Send(query);
    }

    [HttpPut($"/{EntityName}/{{productVariationId}}")]
    public async Task<IActionResult> UpdateProductVariation(string productVariationId)
    {
        var query = new UpdateProductVariationCommand(GetRequest(), productVariationId);

        return await _mediator.Send(query);
    }

    [HttpDelete($"/{EntityName}/{{productVariationId}}")]
    public async Task<IActionResult> DeleteProductVariation(string productVariationId)
    {
        var query = new DeleteProductVariationCommand(GetRequest(), productVariationId);

        return await _mediator.Send(query);
    }

    private HttpRequest GetRequest()
    {
        return _contextAccessor.HttpContext!.Request;
    }
}
