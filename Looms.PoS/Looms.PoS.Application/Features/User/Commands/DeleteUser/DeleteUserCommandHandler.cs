using Looms.PoS.Application.Helpers;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.RequestHandler;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.User.Commands.DeleteUser;

public class DeleteUserCommandHandler : ILoomsRequestHandler<DeleteUserCommand, IActionResult>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUserModelsResolver _modelsResolver;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IPermissionService _permissionService;

    public DeleteUserCommandHandler(
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

    public async Task<IActionResult> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var businessId = HttpContextHelper.GetHeaderBusinessId(command.Request);
        var originalDao = await _usersRepository.GetByBusinessAsync(Guid.Parse(command.Id), Guid.Parse(businessId));

        var deletedDao = _modelsResolver.GetDeletedDao(originalDao);
        _ = await _usersRepository.UpdateAsync(deletedDao);

        return new NoContentResult();
    }

    public void ValidatePermissions(DeleteUserCommand command)
    {
        _permissionService.CheckPermissions(command.Request, [UserRole.SuperAdmin, UserRole.BusinessOwner]);
    }
}