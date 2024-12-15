using Looms.PoS.Application.Features.Product.Commands.CreateProductVariation;
using Looms.PoS.Application.Features.Product.Commands.DeleteProductVariation;
using Looms.PoS.Application.Features.Product.Commands.UpdateProductVariation;
using Looms.PoS.Application.Features.Product.Queries.GetProductVariation;
using Looms.PoS.Application.Features.Product.Queries.GetProductVariationForProduct;
using Looms.PoS.Application.Features.Product.Queries.GetProductVariations;
using Looms.PoS.Application.Models.Requests.ProductVariation;
using Looms.PoS.Application.Models.Responses.Product;
using Looms.PoS.Swagger.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerRequestType(typeof(CreateProductVariationRequest))]
    [SwaggerResponse(StatusCodes.Status201Created, "Product variation successfully created.", typeof(ProductVariationResponse))]
    public async Task<IActionResult> CreateProductVariation()
    {
        var command = new CreateProductVariationCommand(GetRequest());

        return await _mediator.Send(command);
    }

    [HttpGet($"/{EntityName}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Product variations successfully retrieved.", typeof(IEnumerable<ProductVariationResponse>))]
    public async Task<IActionResult> GetProductVariations()
    {
        var query = new GetProductVariationsQuery(GetRequest());

        return await _mediator.Send(query);
    }

    [HttpGet($"/product/{{productId}}/{EntityName}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Product variations successfully retrieved.", typeof(IEnumerable<ProductVariationResponse>))]
    public async Task<IActionResult> GetProductVariations(string productId)
    {
        var query = new GetProductVariationForProductQuery(GetRequest(), productId);
        return await _mediator.Send(query);
    }

    [HttpGet($"/{EntityName}/{{productVariationId}}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Product variations for specific product successfully retrieved.", typeof(IEnumerable<ProductVariationResponse>))]
    public async Task<IActionResult> GetProductVariation(string productVariationId)
    {
        var query = new GetProductVariationQuery(GetRequest(), productVariationId);

        return await _mediator.Send(query);
    }

    [HttpPut($"/{EntityName}/{{productVariationId}}")]
    [SwaggerRequestType(typeof(UpdateProductVariationRequest))]
    [SwaggerResponse(StatusCodes.Status200OK, "Product variation successfully updated.", typeof(ProductVariationResponse))]
    public async Task<IActionResult> UpdateProductVariation(string productVariationId)
    {
        var query = new UpdateProductVariationCommand(GetRequest(), productVariationId);

        return await _mediator.Send(query);
    }

    [HttpDelete($"/{EntityName}/{{productVariationId}}")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Product variation successfully deleted.")]
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
