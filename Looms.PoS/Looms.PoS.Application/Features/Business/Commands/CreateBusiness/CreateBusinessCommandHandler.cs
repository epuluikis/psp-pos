using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Business.Commands.CreateBusiness;

public class CreateBusinessCommandHandler : IRequestHandler<CreateBusinessCommand, IActionResult>
{
    private readonly IBusinessesRepository _businessesRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IBusinessModelsResolver _modelsResolver;

    public CreateBusinessCommandHandler(
        IBusinessesRepository businessesRepository,
        IHttpContentResolver httpContentResolver,
        IBusinessModelsResolver modelsResolver)
    {
        _businessesRepository = businessesRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(CreateBusinessCommand command, CancellationToken cancellationToken)
    {
        var businessRequest = await _httpContentResolver.GetPayloadAsync<CreateBusinessRequest>(command.Request);

        var businessDao = _modelsResolver.GetDaoFromRequest(businessRequest);
        var createdBusinessDao = await _businessesRepository.CreateAsync(businessDao);

        var response = _modelsResolver.GetResponseFromDao(createdBusinessDao);

        return new CreatedAtRouteResult($"/businesses{businessDao.Id}", response);
    }
}
