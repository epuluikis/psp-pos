using Looms.PoS.Application.Helpers;
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

    public async Task<IActionResult> Handle(GetTaxQuery query, CancellationToken cancellationToken)
    {
        var taxDao = await _taxesRepository.GetAsyncByIdAndBusinessId(
            Guid.Parse(query.Id),
            Guid.Parse(HttpContextHelper.GetHeaderBusinessId(query.Request))
        );

        var response = _modelsResolver.GetResponseFromDao(taxDao);

        return new OkObjectResult(response);
    }
}
