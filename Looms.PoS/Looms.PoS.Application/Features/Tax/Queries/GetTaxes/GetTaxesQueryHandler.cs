using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Tax.Queries.GetTaxes;

public class GetTaxesQueryHandler : IRequestHandler<GetTaxesQuery, IActionResult>
{
    private readonly ITaxesRepository _taxesRepository;
    private readonly ITaxModelsResolver _modelsResolver;

    public GetTaxesQueryHandler(ITaxesRepository taxesRepository, ITaxModelsResolver modelsResolver)
    {
        _taxesRepository = taxesRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetTaxesQuery request, CancellationToken cancellationToken)
    {
        var taxDaos = await _taxesRepository.GetAllAsync();

        var response = _modelsResolver.GetResponseFromDao(taxDaos);

        return new OkObjectResult(response);
    }
}
