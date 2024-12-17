using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Product;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace Looms.PoS.Application.Features.Product.Commands.CreateProductVariation;

public class CreateProductVariationCommandHandler : IRequestHandler<CreateProductVariationCommand, IActionResult>
{
    private readonly IProductVariationRepository _productVariationRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IProductVariationModelsResolver _modelsResolver;

    public CreateProductVariationCommandHandler(
        IProductVariationRepository productVariationRepository,
        IHttpContentResolver httpContentResolver,
        IProductVariationModelsResolver modelsResolver)
    {
        _productVariationRepository = productVariationRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(CreateProductVariationCommand command, CancellationToken cancellationToken)
    {
        var productVariationRequest = await _httpContentResolver.GetPayloadAsync<CreateProductVariationRequest>(command.Request);
        var createdProductVariationDao = _modelsResolver.GetDaoFromRequest(productVariationRequest);

        var productVariationDao = await _productVariationRepository.CreateAsync(createdProductVariationDao);
        var response = _modelsResolver.GetResponseFromDao(productVariationDao);

        return new CreatedAtRouteResult($"/productvariations/{createdProductVariationDao.Id}", response);
    }
}
