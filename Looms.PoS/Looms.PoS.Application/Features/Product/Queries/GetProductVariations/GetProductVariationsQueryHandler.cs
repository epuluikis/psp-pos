using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Product.Queries.GetProductVariations;

public class GetProductVariationsQueryHandler : IRequestHandler<GetProductVariationsQuery, IActionResult>
{
    private readonly IProductVariationRepository _productVariationRepository;
    private readonly IProductVariationModelsResolver _modelsResolver;

    public GetProductVariationsQueryHandler(IProductVariationRepository productsRepository, IProductVariationModelsResolver modelsResolver)
    {
        _productVariationRepository = productsRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetProductVariationsQuery request, CancellationToken cancellationToken)
    {
        var productVariationDaos = await _productVariationRepository.GetAllAsync();

        var response = _modelsResolver.GetResponseFromDao(productVariationDaos);

        return new OkObjectResult(response);
    }
}
