using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Service.Commands.DeleteService;

public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, IActionResult>
{
    private readonly IServicesRepository _servicesRepository;
    private readonly IServiceModelsResolver _modelsResolver;

    public DeleteServiceCommandHandler(
        IServicesRepository servicesRepository,
        IServiceModelsResolver modelsResolver)
    {
        _servicesRepository = servicesRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(DeleteServiceCommand command, CancellationToken cancellationToken)
    {
        var originalDao = await _servicesRepository.GetAsync(Guid.Parse(command.Id));

        var serviceDao = _modelsResolver.GetDeletedDao(originalDao);
        _ = await _servicesRepository.UpdateAsync(serviceDao);

        return new NoContentResult();
    }
}