using Looms.PoS.Application.Features.Business.Commands.CreateBusiness;
using Looms.PoS.Application.Features.Business.Queries.GetBusinesses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class DiscountsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _contextAccessor;

    private const string EntityName = "discounts";

    public DiscountsController(IMediator mediator, IHttpContextAccessor contextAccessor)
    {
        _mediator = mediator;
        _contextAccessor = contextAccessor;
    }

    [HttpGet($"/{EntityName}")]
    public async Task<IActionResult> GetDiscounts()
    {
        var query = new GetBusinessesQuery();

        return await _mediator.Send(query);
    }

    private HttpRequest GetRequest()
    {
        return _contextAccessor.HttpContext!.Request;
    }
}
