using Looms.PoS.Application.Features.Service.Commands.CreateService;
using Looms.PoS.Application.Features.Service.Commands.DeleteService;
using Looms.PoS.Application.Features.Service.Commands.UpdateService;
using Looms.PoS.Application.Features.Service.Queries.GetService;
using Looms.PoS.Application.Features.Service.Queries.GetServices;
using Looms.PoS.Application.Models.Requests.Service;
using Looms.PoS.Application.Models.Responses.Service;
using Looms.PoS.Swagger.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerRequestType(typeof(CreateServiceRequest))]
    [SwaggerResponse(StatusCodes.Status201Created, "Service successfully created.", typeof(ServiceResponse))]
    public async Task<IActionResult> CreateService()
    {
        var command = new CreateServiceCommand(GetRequest());

        return await _mediator.Send(command);
    }

    [HttpGet($"/{EntityName}")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of Services returned successfully.", typeof(List<ServiceResponse>))]
    public async Task<IActionResult> GetServices()
    {
        var query = new GetServicesQuery(GetRequest());

        return await _mediator.Send(query);
    }

    [HttpGet($"/{EntityName}/{{serviceId}}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Service details returned successfully.", typeof(ServiceResponse))]
    public async Task<IActionResult> GetService(string serviceId)
    {
        var query = new GetServiceQuery(GetRequest(), serviceId);

        return await _mediator.Send(query);
    }

    [HttpPut($"/{EntityName}/{{serviceId}}")]
    [SwaggerRequestType(typeof(UpdateServiceRequest))]
    [SwaggerResponse(StatusCodes.Status200OK, "Service successfully updated.", typeof(ServiceResponse))]
    public async Task<IActionResult> UpdateService(string serviceId)
    {
        var query = new UpdateServiceCommand(GetRequest(), serviceId);

        return await _mediator.Send(query);
    }

    [HttpDelete($"/{EntityName}/{{serviceId}}")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Service successfully deleted.")]
    public async Task<IActionResult> DeleteService(string serviceId)
    {
        var query = new DeleteServiceCommand(GetRequest(), serviceId);

        return await _mediator.Send(query);
    }

    private HttpRequest GetRequest()
    {
        return _contextAccessor.HttpContext!.Request;
    }
}
