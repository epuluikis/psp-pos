using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.RequestHandler;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Business.Commands.DeleteBusiness;

public class DeleteBusinessCommandHandler : ILoomsRequestHandler<DeleteBusinessCommand, IActionResult>
{
    private readonly IBusinessesRepository _businessesRepository;
    private readonly IBusinessModelsResolver _modelsResolver;
    private readonly IPermissionService _permissionService;

    public DeleteBusinessCommandHandler(
        IBusinessesRepository businessesRepository,
        IBusinessModelsResolver modelsResolver,
        IPermissionService permissionService)
    {
        _businessesRepository = businessesRepository;
        _modelsResolver = modelsResolver;
        _permissionService = permissionService;
    }

    public async Task<IActionResult> Handle(DeleteBusinessCommand command, CancellationToken cancellationToken)
    {
        var originalDao = await _businessesRepository.GetAsync(Guid.Parse(command.Id));

        var businessDao = _modelsResolver.GetDeletedDao(originalDao);
        _ = await _businessesRepository.UpdateAsync(businessDao);

        return new NoContentResult();
    }

    public void ValidatePermissions(DeleteBusinessCommand command)
    {
        _permissionService.CheckPermissions(command.Request, [UserRole.SuperAdmin]);
    }
}