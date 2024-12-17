using Looms.PoS.Application.Helpers;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Service.Queries.GetServices;

public class GetServicesQueryHandler : IRequestHandler<GetServicesQuery, IActionResult>
{
    private readonly IServicesRepository _servicesRepository;
    private readonly IServiceModelsResolver _modelsResolver;

    public GetServicesQueryHandler(IServicesRepository servicesRepository, IServiceModelsResolver modelsResolver)
    {
        _servicesRepository = servicesRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetServicesQuery request, CancellationToken cancellationToken)
    {
        var serviceDaos = await _servicesRepository.GetAllAsyncByBusinessId(
            Guid.Parse(HttpContextHelper.GetHeaderBusinessId(request.Request))
        );

        var response = _modelsResolver.GetResponseFromDao(serviceDaos);

        return new OkObjectResult(response);
    }
}
