using Looms.PoS.Application.Features.Product.Commands.CreateProduct;
using Looms.PoS.Application.Features.Product.Commands.DeleteProduct;
using Looms.PoS.Application.Features.Product.Commands.UpdateProduct;
using Looms.PoS.Application.Features.Product.Queries.GetProduct;
using Looms.PoS.Application.Features.Product.Queries.GetProducts;
using Looms.PoS.Application.Models.Requests.Product;
using Looms.PoS.Application.Models.Responses.Product;
using Looms.PoS.Swagger.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Looms.PoS.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _contextAccessor;

    private const string EntityName = "products";

    public ProductsController(IMediator mediator, IHttpContextAccessor contextAccessor)
    {
        _mediator = mediator;
        _contextAccessor = contextAccessor;
    }

    [HttpPost($"/{EntityName}")]
    [SwaggerRequestType(typeof(CreateProductRequest))]
    [SwaggerResponse(StatusCodes.Status201Created, "Product successfully created.", typeof(ProductResponse))]
    public async Task<IActionResult> CreateProduct()
    {
        var command = new CreateProductCommand(GetRequest());

        return await _mediator.Send(command);
    }

    [HttpGet($"/{EntityName}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Products successfully retrieved.", typeof(IEnumerable<ProductResponse>))]
    public async Task<IActionResult> GetProducts()
    {
        var query = new GetProductsQuery(GetRequest());

        return await _mediator.Send(query);
    }

    [HttpGet($"/{EntityName}/{{productId}}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Product successfully retrieved.", typeof(ProductResponse))]
    public async Task<IActionResult> GetProducts(string productId)
    {
        var query = new GetProductQuery(GetRequest(), productId);

        return await _mediator.Send(query);
    }

    [HttpPut($"/{EntityName}/{{productId}}")]
    [SwaggerRequestType(typeof(UpdateProductRequest))]
    [SwaggerResponse(StatusCodes.Status200OK, "Product successfully updated.", typeof(ProductResponse))]
    public async Task<IActionResult> UpdateProduct(string productId)
    {
        var query = new UpdateProductCommand(GetRequest(), productId);

        return await _mediator.Send(query);
    }

    [HttpDelete($"/{EntityName}/{{productId}}")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Product successfully deleted.")]
    public async Task<IActionResult> DeleteProduct(string productId)
    {
        var query = new DeleteProductCommand(GetRequest(), productId);

        return await _mediator.Send(query);
    }

    private HttpRequest GetRequest()
    {
        return _contextAccessor.HttpContext!.Request;
    }
}
