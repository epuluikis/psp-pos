using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.RequestHandler;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Business.Queries.GetBusiness;

public class GetBusinessQueryHandler : ILoomsRequestHandler<GetBusinessQuery, IActionResult>
{
    private readonly IBusinessesRepository _businessesRepository;
    private readonly IBusinessModelsResolver _modelsResolver;
    private readonly IPermissionService _permissionService;

    public GetBusinessQueryHandler(
        IBusinessesRepository businessesRepository,
        IBusinessModelsResolver modelsResolver,
        IPermissionService permissionService)
    {
        _businessesRepository = businessesRepository;
        _modelsResolver = modelsResolver;
        _permissionService = permissionService;
    }

    public async Task<IActionResult> Handle(GetBusinessQuery query, CancellationToken cancellationToken)
    {
        var businessDao = await _businessesRepository.GetAsync(Guid.Parse(query.Id));

        var response = _modelsResolver.GetResponseFromDao(businessDao);

        return new OkObjectResult(response);
    }

    public void ValidatePermissions(GetBusinessQuery query)
    {
        _permissionService.CheckPermissions(query.Request, [UserRole.SuperAdmin]);
    }
}
