using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.RequestHandler;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Business.Queries.GetBusinesses;

public class GetBusinessesQueryHandler : ILoomsRequestHandler<GetBusinessesQuery, IActionResult>
{
    private readonly IBusinessesRepository _businessesRepository;
    private readonly IBusinessModelsResolver _modelsResolver;
    private readonly IPermissionService _permissionService;

    public GetBusinessesQueryHandler(
        IBusinessesRepository businessesRepository,
        IBusinessModelsResolver modelsResolver,
        IPermissionService permissionService)
    {
        _businessesRepository = businessesRepository;
        _modelsResolver = modelsResolver;
        _permissionService = permissionService;
    }

    public async Task<IActionResult> Handle(GetBusinessesQuery query, CancellationToken cancellationToken)
    {
        var businessDaos = await _businessesRepository.GetAllAsync();

        var response = _modelsResolver.GetResponseFromDao(businessDaos);

        return new OkObjectResult(response);
    }

    public void ValidatePermissions(GetBusinessesQuery query)
    {
        _permissionService.CheckPermissions(query.Request, [UserRole.SuperAdmin]);
    }
}
