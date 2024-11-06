using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Business.Queries.GetBusiness;

public class GetBusinessQueryHandler : IRequestHandler<GetBusinessQuery, IActionResult>
{
    private readonly IBusinessesRepository _businessesRepository;
    private readonly IBusinessModelsResolver _modelsResolver;

    public GetBusinessQueryHandler(IBusinessesRepository businessesRepository, IBusinessModelsResolver modelsResolver)
    {
        _businessesRepository = businessesRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetBusinessQuery request, CancellationToken cancellationToken)
    {
        var businessDao = await _businessesRepository.GetAsync(Guid.Parse(request.Id));

        var response = _modelsResolver.GetResponseFromDao(businessDao);

        return new OkObjectResult(response);
    }
}
