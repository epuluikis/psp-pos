using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Tax;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Tax.Commands.CreateTax;

public class CreateTaxCommandHandler : IRequestHandler<CreateTaxCommand, IActionResult>
{
    private readonly ITaxesRepository _taxesRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly ITaxModelsResolver _modelsResolver;

    public CreateTaxCommandHandler(
        ITaxesRepository taxesRepository,
        IHttpContentResolver httpContentResolver,
        ITaxModelsResolver modelsResolver)
    {
        _taxesRepository = taxesRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(CreateTaxCommand command, CancellationToken cancellationToken)
    {
        var taxRequest = await _httpContentResolver.GetPayloadAsync<CreateTaxRequest>(command.Request);

        var taxDao = _modelsResolver.GetDaoFromRequest(taxRequest);
        var createdTaxDao = await _taxesRepository.CreateAsync(taxDao);

        var response = _modelsResolver.GetResponseFromDao(createdTaxDao);

        return new CreatedAtRouteResult($"/taxes{taxDao.Id}", response);
    }
}
