using Looms.PoS.Application.Features.User.Commands.CreateUser;
using Looms.PoS.Application.Features.User.Commands.DeleteUser;
using Looms.PoS.Application.Features.User.Commands.UpdateUser;
using Looms.PoS.Application.Features.User.Queries.GetUser;
using Looms.PoS.Application.Features.User.Queries.GetUsers;
using Looms.PoS.Application.Models.Requests.User;
using Looms.PoS.Application.Models.Responses.User;
using Looms.PoS.Swagger.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Looms.PoS.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHttpContextAccessor _contextAccessor;

    private const string EntityName = "users";

    public UsersController(IMediator mediator, IHttpContextAccessor contextAccessor)
    {
        _mediator = mediator;
        _contextAccessor = contextAccessor;
    }

    [HttpPost($"/{EntityName}")]
    [SwaggerRequestType(typeof(CreateUserRequest))]
    [SwaggerResponse(StatusCodes.Status201Created, "User successfully created.", typeof(UserResponse))]
    public async Task<IActionResult> CreateUser()
    {
        var comnand = new CreateUserCommand(GetRequest());

        return await _mediator.Send(comnand);
    }

    [HttpGet($"/{EntityName}")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of users returned successfully.", typeof(List<UserResponse>))]
    public async Task<IActionResult> GetUsers()
    {
        var query = new GetUsersQuery(GetRequest());

        return await _mediator.Send(query);
    }

    [HttpGet($"/{EntityName}/{{userId}}")]
    [SwaggerResponse(StatusCodes.Status200OK, "User details returned successfully.", typeof(UserResponse))]
    public async Task<IActionResult> GetUser(string userId)
    {
        var query = new GetUserQuery(GetRequest(), userId);

        return await _mediator.Send(query);
    }

    [HttpPut($"/{EntityName}/{{userId}}")]
    [SwaggerResponse(StatusCodes.Status200OK, "User updated successfully.", typeof(UserResponse))]
    public async Task<IActionResult> UpdateUser(string userId)
    {
        var query = new UpdateUserCommand(GetRequest(), userId);

        return await _mediator.Send(query);
    }

    [HttpDelete($"/{EntityName}/{{userId}}")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "User deleted successfully.")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var query = new DeleteUserCommand(GetRequest(), userId);

        return await _mediator.Send(query);
    }

    private HttpRequest GetRequest()
    {
        return _contextAccessor.HttpContext!.Request;
    }
}
