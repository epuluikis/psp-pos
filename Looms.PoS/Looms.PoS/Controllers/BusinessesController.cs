﻿using Looms.PoS.Application.Features.Business.Commands.CreateBusiness;
using Looms.PoS.Application.Features.Business.Commands.DeleteBusiness;
using Looms.PoS.Application.Features.Business.Commands.UpdateBusiness;
using Looms.PoS.Application.Features.Business.Queries.GetBusiness;
using Looms.PoS.Application.Features.Business.Queries.GetBusinesses;
using Looms.PoS.Application.Models.Requests.Business;
using Looms.PoS.Application.Models.Responses.Business;
using Looms.PoS.Swagger.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Looms.PoS.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class BusinessesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _contextAccessor;

    private const string EntityName = "businesses";

    public BusinessesController(IMediator mediator, IHttpContextAccessor contextAccessor)
    {
        _mediator = mediator;
        _contextAccessor = contextAccessor;
    }

    [HttpPost($"/{EntityName}")]
    [SwaggerRequestType(typeof(CreateBusinessRequest))]
    [SwaggerResponse(StatusCodes.Status201Created, "Business successfully created.", typeof(BusinessResponse))]
    public async Task<IActionResult> CreateBusiness()
    {
        var comnand = new CreateBusinessCommand(GetRequest());

        return await _mediator.Send(comnand);
    }

    [HttpGet($"/{EntityName}")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of businesses returned successfully.", typeof(List<BusinessResponse>))]
    public async Task<IActionResult> GetBusinesses()
    {
        var query = new GetBusinessesQuery(GetRequest());

        return await _mediator.Send(query);
    }

    [HttpGet($"/{EntityName}/{{businessId}}")]
    [SwaggerResponse(StatusCodes.Status200OK, "Business details returned successfully.", typeof(BusinessResponse))]
    public async Task<IActionResult> GetBusiness(string businessId)
    {
        var query = new GetBusinessQuery(GetRequest(), businessId);

        return await _mediator.Send(query);
    }

    [HttpPut($"/{EntityName}/{{businessId}}")]
    [SwaggerRequestType(typeof(UpdateBusinessRequest))]
    [SwaggerResponse(StatusCodes.Status200OK, "Business successfully updated.", typeof(BusinessResponse))]
    public async Task<IActionResult> UpdateBusiness(string businessId)
    {
        var query = new UpdateBusinessCommand(GetRequest(), businessId);

        return await _mediator.Send(query);
    }

    [HttpDelete($"/{EntityName}/{{businessId}}")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Business successfully deleted.")]
    public async Task<IActionResult> DeleteBusiness(string businessId)
    {
        var query = new DeleteBusinessCommand(GetRequest(), businessId);

        return await _mediator.Send(query);
    }

    private HttpRequest GetRequest()
    {
        return _contextAccessor.HttpContext!.Request;
    }
}
