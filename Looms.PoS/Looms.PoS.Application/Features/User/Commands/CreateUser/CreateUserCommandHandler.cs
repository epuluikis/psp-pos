using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.RequestHandler;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Application.Models.Requests.User;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.User.Commands.CreateUser;

public class CreateUserCommandHandler : ILoomsRequestHandler<CreateUserCommand, IActionResult>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUserModelsResolver _modelsResolver;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IPermissionService _permissionService;

    public CreateUserCommandHandler(
        IUsersRepository userRepository,
        IUserModelsResolver modelsResolver,
        IHttpContentResolver httpContentResolver,
        IPermissionService permissionService)
    {
        _usersRepository = userRepository;
        _modelsResolver = modelsResolver;
        _httpContentResolver = httpContentResolver;
        _permissionService = permissionService;
    }

    public async Task<IActionResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var userRequest = await _httpContentResolver.GetPayloadAsync<CreateUserRequest>(command.Request);

        var userDao = _modelsResolver.GetDaoFromRequest(userRequest);
        var createdUserDao = await _usersRepository.CreateAsync(userDao);

        var response = _modelsResolver.GetResponseFromDao(createdUserDao);

        return new CreatedAtRouteResult($"/users/{createdUserDao.Id}", response);
    }

    public void ValidatePermissions(CreateUserCommand command)
    {
        _permissionService.CheckPermissions(command.Request, [UserRole.SuperAdmin, UserRole.BusinessOwner]);
    }
}
