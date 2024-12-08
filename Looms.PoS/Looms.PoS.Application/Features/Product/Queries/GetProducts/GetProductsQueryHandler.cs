using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Product.Queries.GetProducts;

public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IActionResult>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IProductModelsResolver _modelsResolver;

    public GetProductsQueryHandler(IProductsRepository productsRepository, IProductModelsResolver modelsResolver)
    {
        _productsRepository = productsRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var productDaos = await _productsRepository.GetAllAsync();

        var response = _modelsResolver.GetResponseFromDao(productDaos);

        return new OkObjectResult(response);
    }
}
