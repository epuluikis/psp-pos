using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Tax.Queries.GetTax;

public class GetTaxQueryHandler : IRequestHandler<GetTaxQuery, IActionResult>
{
    private readonly ITaxesRepository _taxesRepository;
    private readonly ITaxModelsResolver _modelsResolver;

    public GetTaxQueryHandler(ITaxesRepository taxesRepository, ITaxModelsResolver modelsResolver)
    {
        _taxesRepository = taxesRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetTaxQuery request, CancellationToken cancellationToken)
    {
        var taxDao = await _taxesRepository.GetAsync(Guid.Parse(request.Id));

        var response = _modelsResolver.GetResponseFromDao(taxDao);

        return new OkObjectResult(response);
    }
}
