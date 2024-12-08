using Looms.PoS.Application.Features.Auth.Commands.Login;
using Looms.PoS.Application.Models.Requests.Auth;
using Looms.PoS.Application.Models.Responses.Auth;
using Looms.PoS.Swagger.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Looms.PoS.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _contextAccessor;

    private const string EntityName = "auth";

    public AuthController(IMediator mediator, IHttpContextAccessor contextAccessor)
    {
        _mediator = mediator;
        _contextAccessor = contextAccessor;
    }

    [HttpPost($"/{EntityName}/generate-token")]
    [SwaggerRequestType(typeof(LoginRequest))]
    [SwaggerResponse(StatusCodes.Status200OK, "Token successfully generated.", typeof(LoginResponse))]
    public async Task<IActionResult> GenerateToken()
    {
        var comnand = new LoginCommand(GetRequest());

        return await _mediator.Send(comnand);
    }

    private HttpRequest GetRequest()
    {
        return _contextAccessor.HttpContext!.Request;
    }
}
