using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.RequestHandler;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Application.Models.Requests.Business;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Business.Commands.UpdateBusiness;
public class UpdateBusinessCommandHandler : ILoomsRequestHandler<UpdateBusinessCommand, IActionResult>
{
    private readonly IBusinessesRepository _businessesRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IBusinessModelsResolver _modelsResolver;
    private readonly IPermissionService _permissionService;

    public UpdateBusinessCommandHandler(
        IBusinessesRepository businessesRepository,
        IHttpContentResolver httpContentResolver,
        IBusinessModelsResolver modelsResolver,
        IPermissionService permissionService)
    {
        _businessesRepository = businessesRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
        _permissionService = permissionService;
    }

    public async Task<IActionResult> Handle(UpdateBusinessCommand command, CancellationToken cancellationToken)
    {
        var updateBusinessRequest = await _httpContentResolver.GetPayloadAsync<UpdateBusinessRequest>(command.Request);

        var originalDao = await _businessesRepository.GetAsync(Guid.Parse(command.Id));

        var businessDao = _modelsResolver.GetDaoFromDaoAndRequest(originalDao, updateBusinessRequest);
        var updatedBusinessDao = await _businessesRepository.UpdateAsync(businessDao);

        var response = _modelsResolver.GetResponseFromDao(updatedBusinessDao);

        return new OkObjectResult(response);
    }

    public void ValidatePermissions(UpdateBusinessCommand command)
    {
        _permissionService.CheckPermissions(command.Request, [UserRole.SuperAdmin]);
    }
}
