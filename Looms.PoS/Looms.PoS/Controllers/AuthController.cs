using Looms.PoS.Application.Features.Auth.Commands.Login;
using Looms.PoS.Application.Models.Requests.Auth;
using Looms.PoS.Application.Models.Responses.Auth;
using Looms.PoS.Configuration.Attributes;
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
    [ExcludeHeader]
    [SwaggerRequestType(typeof(LoginRequest))]
    [SwaggerResponse(StatusCodes.Status200OK, "Token successfully generated.", typeof(LoginResponse))]
    public async Task<IActionResult> GenerateToken()
    {
        var comnand = new LoginCommand(GetRequest());

        var response = await _mediator.Send(comnand);

        if (response is LoginResponse loginResponse)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Expires = loginResponse.Expires,
            };

            Response.Cookies.Append("auth-token", loginResponse.Token, cookieOptions);
        }

        return response;
    }

    private HttpRequest GetRequest()
    {
        return _contextAccessor.HttpContext!.Request;
    }
}
