﻿using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.RequestHandler;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Application.Models.Requests.Business;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Business.Commands.CreateBusiness;

public class CreateBusinessCommandHandler : ILoomsRequestHandler<CreateBusinessCommand, IActionResult>
{
    private readonly IBusinessesRepository _businessesRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IBusinessModelsResolver _modelsResolver;
    private readonly IPermissionService _permissionService;

    public CreateBusinessCommandHandler(
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

    public async Task<IActionResult> Handle(CreateBusinessCommand command, CancellationToken cancellationToken)
    {
        var businessRequest = await _httpContentResolver.GetPayloadAsync<CreateBusinessRequest>(command.Request);

        var businessDao = _modelsResolver.GetDaoFromRequest(businessRequest);
        businessDao = await _businessesRepository.CreateAsync(businessDao);

        var response = _modelsResolver.GetResponseFromDao(businessDao);

        return new CreatedAtRouteResult($"/businesses/{businessDao.Id}", response);
    }

    public void ValidatePermissions(CreateBusinessCommand command)
    {
        _permissionService.CheckPermissions(command.Request, [UserRole.SuperAdmin]);
    }
}
