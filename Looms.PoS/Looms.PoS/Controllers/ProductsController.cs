using Looms.PoS.Application.Features.Product.Commands.CreateProduct;
using Looms.PoS.Application.Features.Product.Commands.DeleteProduct;
using Looms.PoS.Application.Features.Product.Commands.UpdateProduct;
using Looms.PoS.Application.Features.Product.Queries.GetProduct;
using Looms.PoS.Application.Features.Product.Queries.GetProducts;
using Looms.PoS.Application.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> CreateProduct()
    {
        var command = new CreateProductCommand(GetRequest());

        return await _mediator.Send(command);
    }

    [HttpGet($"/{EntityName}")]
    public async Task<IActionResult> GetProducts()
    {
        var query = new GetProductsQuery(GetRequest());

        return await _mediator.Send(query);
    }

    [HttpGet($"/{EntityName}/{{productId}}")]
    public async Task<IActionResult> GetProducts(string productId)
    {
        var query = new GetProductQuery(GetRequest(), productId);

        return await _mediator.Send(query);
    }

    [HttpPut($"/{EntityName}/{{productId}}")]
    public async Task<IActionResult> UpdateProduct(string productId)
    {
        var query = new UpdateProductCommand(GetRequest(), productId);

        return await _mediator.Send(query);
    }

    [HttpDelete($"/{EntityName}/{{productId}}")]
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
