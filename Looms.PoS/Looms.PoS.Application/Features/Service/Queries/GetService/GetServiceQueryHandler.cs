using Looms.PoS.Application.Helpers;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Service.Queries.GetService;

public class GetServiceQueryHandler : IRequestHandler<GetServiceQuery, IActionResult>
{
    private readonly IServicesRepository _servicesRepository;
    private readonly IServiceModelsResolver _modelsResolver;

    public GetServiceQueryHandler(IServicesRepository servicesRepository, IServiceModelsResolver modelsResolver)
    {
        _servicesRepository = servicesRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetServiceQuery request, CancellationToken cancellationToken)
    {
        var serviceDao = await _servicesRepository.GetAsyncByIdAndBusinessId(
            Guid.Parse(request.Id),
            Guid.Parse(HttpContextHelper.GetHeaderBusinessId(request.Request))
        );

        var response = _modelsResolver.GetResponseFromDao(serviceDao);

        return new OkObjectResult(response);
    }
}
