using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Business.Commands.DeleteBusiness;

public class DeleteBusinessCommandHandler : IRequestHandler<DeleteBusinessCommand, IActionResult>
{
    private readonly IBusinessesRepository _businessesRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IBusinessModelsResolver _modelsResolver;

    public DeleteBusinessCommandHandler(
        IBusinessesRepository businessesRepository,
        IHttpContentResolver httpContentResolver,
        IBusinessModelsResolver modelsResolver)
    {
        _businessesRepository = businessesRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(DeleteBusinessCommand command, CancellationToken cancellationToken)
    {
        var originalDao = await _businessesRepository.GetAsync(Guid.Parse(command.Id));

        var businessDao = _modelsResolver.GetDeletedDao(originalDao);
        _ = await _businessesRepository.UpdateAsync(businessDao);

        return new NoContentResult();
    }
}