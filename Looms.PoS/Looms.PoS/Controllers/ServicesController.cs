using Looms.PoS.Application.Features.Service.Commands.CreateService;
using Looms.PoS.Application.Features.Service.Queries.GetService;
using Looms.PoS.Application.Features.Service.Queries.GetServices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _contextAccessor;

    private const string EntityName = "services";

    public ServicesController(IMediator mediator, IHttpContextAccessor contextAccessor)
    {
        _mediator = mediator;
        _contextAccessor = contextAccessor;
    }

    [HttpPost($"/{EntityName}")]
    public async Task<IActionResult> CreateService()
    {
        var command = new CreateServiceCommand(GetRequest());

        return await _mediator.Send(command);
    }

    [HttpGet($"/{EntityName}")]
    public async Task<IActionResult> GetServices()
    {
        var query = new GetServicesQuery(GetRequest());

        return await _mediator.Send(query);
    }

    [HttpGet($"/{EntityName}/{{serviceId}}")]
    public async Task<IActionResult> GetService(string serviceId)
    {
        var query = new GetServiceQuery(GetRequest(), serviceId);

        return await _mediator.Send(query);
    }

    private HttpRequest GetRequest()
    {
        return _contextAccessor.HttpContext!.Request;
    }
}
