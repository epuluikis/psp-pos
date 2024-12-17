using Looms.PoS.Application.Helpers;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.RequestHandler;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.User.Queries.GetUsers;

public class GetUsersQueryHandler : ILoomsRequestHandler<GetUsersQuery, IActionResult>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUserModelsResolver _modelsResolver;
    private readonly IPermissionService _permissionService;

    public GetUsersQueryHandler(
        IUsersRepository usersRepository,
        IUserModelsResolver modelsResolver,
        IPermissionService permissionService)
    {
        _usersRepository = usersRepository;
        _modelsResolver = modelsResolver;
        _permissionService = permissionService;
    }

    public async Task<IActionResult> Handle(GetUsersQuery query, CancellationToken cancellationToken)
    {
        var businessId = HttpContextHelper.GetHeaderBusinessId(query.Request);
        var userDaos = await _usersRepository.GetAllByBusinessAsync(Guid.Parse(businessId));

        var response = _modelsResolver.GetResponseFromDao(userDaos);

        return new OkObjectResult(response);
    }

    public void ValidatePermissions(GetUsersQuery query)
    {
        _permissionService.CheckPermissions(query.Request, [UserRole.SuperAdmin, UserRole.BusinessOwner]);
    }
}
