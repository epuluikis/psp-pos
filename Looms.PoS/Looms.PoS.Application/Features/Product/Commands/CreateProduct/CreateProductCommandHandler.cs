using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Product;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace Looms.PoS.Application.Features.Product.Commands.CreateProduct;
//TODO: include ProductVariation and ProductStock repos
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, IActionResult>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IProductStockRepository _productStockRepository;
    private readonly IProductVariationRepository _productVariationRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IProductModelsResolver _modelsResolver;

    public CreateProductCommandHandler(
        IProductsRepository productsRepository,
        IProductVariationRepository productVariationRepository,
        IProductStockRepository productStockRepository,
        IHttpContentResolver httpContentResolver,
        IProductModelsResolver modelsResolver)
    {
        _productsRepository = productsRepository;
        _productVariationRepository = productVariationRepository;
        _productStockRepository = productStockRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var productRequest = await _httpContentResolver.GetPayloadAsync<CreateProductRequest>(command.Request);

        var productDaos = _modelsResolver.GetDaoFromRequest(productRequest);

        var createdProductDao = await _productsRepository.CreateAsync(productDaos.Item1);
        var createdVariationsDao = await _productVariationRepository.CreateAsync(productDaos.Item2);
        var createdStockDao = await _productStockRepository.CreateAsync(productDaos.Item3);

        //TODO: the response then should be a combination of 3 responses?
        var response = _modelsResolver.GetResponseFromDao(createdProductDao, createdVariationsDao, createdStockDao);

        return new CreatedAtRouteResult($"/products{productDaos.Item1.Id}", response);
    }
}
