using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Product.Queries.GetProduct;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, IActionResult>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IProductModelsResolver _modelsResolver;

    public GetProductQueryHandler(IProductsRepository productsRepository, IProductModelsResolver modelsResolver)
    {
        _productsRepository = productsRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var productDao = await _productsRepository.GetAsync(Guid.Parse(request.Id));

        var response = _modelsResolver.GetResponseFromDao(productDao);

        return new OkObjectResult(response);
    }
}
