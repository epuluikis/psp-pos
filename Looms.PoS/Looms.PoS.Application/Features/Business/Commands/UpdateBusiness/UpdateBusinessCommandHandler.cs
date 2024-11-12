using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Business;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Business.Commands.UpdateBusiness;
public class UpdateBusinessCommandHandler : IRequestHandler<UpdateBusinessCommand, IActionResult>
{
    private readonly IBusinessesRepository _businessesRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IBusinessModelsResolver _modelsResolver;

    public UpdateBusinessCommandHandler(
        IBusinessesRepository businessesRepository,
        IHttpContentResolver httpContentResolver,
        IBusinessModelsResolver modelsResolver)
    {
        _businessesRepository = businessesRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(UpdateBusinessCommand command, CancellationToken cancellationToken)
    {
        var businessRequest = await _httpContentResolver.GetPayloadAsync<UpdateBusinessRequest>(command.Request);

        var originalDao = await _businessesRepository.GetAsync(Guid.Parse(command.Id));

        var businessDao = _modelsResolver.GetDaoFromDaoAndRequest(originalDao, businessRequest);
        var updatedBusinessDao = await _businessesRepository.UpdateAsync(businessDao);

        var response = _modelsResolver.GetResponseFromDao(updatedBusinessDao);

        return new OkObjectResult(response);
    }
}
