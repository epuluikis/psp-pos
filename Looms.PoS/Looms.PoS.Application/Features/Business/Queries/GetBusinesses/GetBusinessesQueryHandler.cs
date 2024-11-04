﻿using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Business.Queries.GetBusinesses;

public class GetBusinessesQueryHandler : IRequestHandler<GetBusinessesQuery, IActionResult>
{
    private readonly IBusinessesRepository _businessesRepository;
    private readonly IBusinessModelsResolver _modelsResolver;

    public GetBusinessesQueryHandler(IBusinessesRepository businessesRepository, IBusinessModelsResolver modelsResolver)
    {
        _businessesRepository = businessesRepository;
        _modelsResolver = modelsResolver;
    }

    public Task<IActionResult> Handle(GetBusinessesQuery request, CancellationToken cancellationToken)
    {
        var businessDaos = _businessesRepository.GetAll();

        var response = _modelsResolver.GetResponseFromDao(businessDaos);

        return Task.FromResult<IActionResult>(new OkObjectResult(response));
    }
}
