using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Service;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Service.Commands.UpdateService;
public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, IActionResult>
{
    private readonly IServicesRepository _servicesRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IServiceModelsResolver _modelsResolver;

    public UpdateServiceCommandHandler(
        IServicesRepository servicesRepository,
        IHttpContentResolver httpContentResolver,
        IServiceModelsResolver modelsResolver)
    {
        _servicesRepository = servicesRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(UpdateServiceCommand command, CancellationToken cancellationToken)
    {
        var updateServiceRequest = await _httpContentResolver.GetPayloadAsync<UpdateServiceRequest>(command.Request);

        var originalDao = await _servicesRepository.GetAsync(Guid.Parse(command.Id));

        var serviceDao = _modelsResolver.GetDaoFromDaoAndRequest(originalDao, updateServiceRequest);
        var updatedServiceDao = await _servicesRepository.UpdateAsync(serviceDao);

        var response = _modelsResolver.GetResponseFromDao(updatedServiceDao);

        return new OkObjectResult(response);
    }
}