using Looms.PoS.Application.Helpers;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Service;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Service.Commands.CreateService;

public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, IActionResult>
{
    private readonly IServicesRepository _servicesRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IServiceModelsResolver _modelsResolver;

    public CreateServiceCommandHandler(
        IServicesRepository servicesRepository,
        IHttpContentResolver httpContentResolver,
        IServiceModelsResolver modelsResolver)
    {
        _servicesRepository = servicesRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(CreateServiceCommand command, CancellationToken cancellationToken)
    {
        var serviceRequest = await _httpContentResolver.GetPayloadAsync<CreateServiceRequest>(command.Request);
        var tax = await GetTaxAsync(productRequest.TaxId);

        var serviceDao = _modelsResolver.GetDaoFromRequest(
            serviceRequest,
            Guid.Parse(HttpContextHelper.GetHeaderBusinessId(command.Request)),
            tax
        );

        var createdServiceDao = await _servicesRepository.CreateAsync(serviceDao);

        var response = _modelsResolver.GetResponseFromDao(createdServiceDao);

        return new CreatedAtRouteResult($"/services/{serviceDao.Id}", response);
    }

    private async Task<TaxDao> GetTaxAsync(string? taxId)
    {
        if (taxId is null)
        {
            return await _taxesRepository.GetByTaxCategoryAsync(TaxCategory.Service);
        }
        else
        {
            return await _taxesRepository.GetAsync(Guid.Parse(taxId));
        }
    }
}
