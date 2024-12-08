using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Tax.Commands.DeleteTax;

public class DeleteTaxCommandHandler : IRequestHandler<DeleteTaxCommand, IActionResult>
{
    private readonly ITaxesRepository _taxesRepository;
    private readonly ITaxModelsResolver _modelsResolver;

    public DeleteTaxCommandHandler(
        ITaxesRepository taxesRepository,
        ITaxModelsResolver modelsResolver)
    {
        _taxesRepository = taxesRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(DeleteTaxCommand command, CancellationToken cancellationToken)
    {
        var originalDao = await _taxesRepository.GetAsync(Guid.Parse(command.Id));

        var taxDao = _modelsResolver.GetDeletedDao(originalDao);
        _ = await _taxesRepository.UpdateAsync(taxDao);

        return new NoContentResult();
    }
}