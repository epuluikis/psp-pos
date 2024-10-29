using Looms.PoS.Application.Features.Business.Queries.GetBusinesses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Controllers;

[ApiController]
[Route("[controller]")]
public class BusinessController : ControllerBase
{
    private readonly IMediator _mediator;

    private const string EntityName = "businesses";

    public BusinessController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet($"/{EntityName}")]
    public async Task<IActionResult> GetBusinesses()
    {
        var query = new GetBusinessesQuery();

        return await _mediator.Send(query);
    }
}
