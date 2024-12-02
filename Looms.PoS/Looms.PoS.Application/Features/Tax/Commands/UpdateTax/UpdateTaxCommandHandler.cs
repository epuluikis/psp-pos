using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Tax;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Tax.Commands.UpdateTax;
public class UpdateTaxCommandHandler : IRequestHandler<UpdateTaxCommand, IActionResult>
{
    private readonly ITaxesRepository _taxesRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly ITaxModelsResolver _modelsResolver;

    public UpdateTaxCommandHandler(
        ITaxesRepository taxesRepository,
        IHttpContentResolver httpContentResolver,
        ITaxModelsResolver modelsResolver)
    {
        _taxesRepository = taxesRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(UpdateTaxCommand command, CancellationToken cancellationToken)
    {
        var updateTaxRequest = await _httpContentResolver.GetPayloadAsync<UpdateTaxRequest>(command.Request);

        var originalDao = await _taxesRepository.GetAsync(Guid.Parse(command.Id));

        var taxDao = _modelsResolver.GetDaoFromDaoAndRequest(originalDao, updateTaxRequest);
        var updatedTaxDao = await _taxesRepository.UpdateAsync(taxDao);

        var response = _modelsResolver.GetResponseFromDao(updatedTaxDao);

        return new OkObjectResult(response);
    }
}
