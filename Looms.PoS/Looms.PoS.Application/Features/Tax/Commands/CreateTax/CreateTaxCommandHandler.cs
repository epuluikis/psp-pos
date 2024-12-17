using Looms.PoS.Application.Helpers;
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
    private readonly ITaxModelsResolver _taxModelsResolver;

    public CreateTaxCommandHandler(
        ITaxesRepository taxesRepository,
        IHttpContentResolver httpContentResolver,
        ITaxModelsResolver taxModelsResolver
    )
    {
        _taxesRepository = taxesRepository;
        _httpContentResolver = httpContentResolver;
        _taxModelsResolver = taxModelsResolver;
    }

    public async Task<IActionResult> Handle(CreateTaxCommand command, CancellationToken cancellationToken)
    {
        var taxRequest = await _httpContentResolver.GetPayloadAsync<CreateTaxRequest>(command.Request);

        var taxDao = _taxModelsResolver.GetDaoFromRequestAndBusinessId(
            taxRequest,
            Guid.Parse(HttpContextHelper.GetHeaderBusinessId(command.Request))
        );

        taxDao = await _taxesRepository.CreateAsync(taxDao);

        var response = _taxModelsResolver.GetResponseFromDao(taxDao);

        return new CreatedAtRouteResult($"/taxes/{taxDao.Id}", response);
    }
}
