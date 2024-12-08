using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Product.Queries.GetProductVariation;

public class GetProductVariationQueryHandler : IRequestHandler<GetProductVariationQuery, IActionResult>
{
    private readonly IProductVariationRepository _productVariationRepository;
    private readonly IProductVariationModelsResolver _modelsResolver;

    public GetProductVariationQueryHandler(IProductVariationRepository productVariationRepository, IProductVariationModelsResolver modelsResolver)
    {
        _productVariationRepository = productVariationRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetProductVariationQuery request, CancellationToken cancellationToken)
    {
        var productVariationDao = await _productVariationRepository.GetAsync(Guid.Parse(request.Id));

        var response = _modelsResolver.GetResponseFromDao(productVariationDao);

        return new OkObjectResult(response);
    }
}
