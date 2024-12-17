using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Product;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Product.Commands.UpdateProductVariation;

public class UpdateProductVariationCommandHandler : IRequestHandler<UpdateProductVariationCommand, IActionResult>
{
    private readonly IProductVariationRepository _productVariationRepository;
    private readonly IProductsRepository _productsRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IProductVariationModelsResolver _modelsResolver;

    public UpdateProductVariationCommandHandler(
        IProductVariationRepository productVariationRepository,
        IProductsRepository productsRepository,
        IHttpContentResolver httpContentResolver,
        IProductVariationModelsResolver modelsResolver)
    {
        _productVariationRepository = productVariationRepository;
        _productsRepository = productsRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(UpdateProductVariationCommand command, CancellationToken cancellationToken)
    {
        var updateProductVariationRequest = await _httpContentResolver.GetPayloadAsync<UpdateProductVariationRequest>(command.Request);

        var originalDao = await _productVariationRepository.GetAsync(Guid.Parse(command.Id));
        var productVariationDao = await GetUpdateProductVariationDaoAsync(originalDao, updateProductVariationRequest);

        var updatedProductVariationDao = await _productVariationRepository.UpdateAsync(productVariationDao);

        var response = _modelsResolver.GetResponseFromDao(updatedProductVariationDao);

        return new OkObjectResult(response);
    }

    private async Task<ProductVariationDao> GetUpdateProductVariationDaoAsync(
        ProductVariationDao originalDao,
        UpdateProductVariationRequest updateProductVariationRequest)
    {
        if (originalDao.ProductId != Guid.Parse(updateProductVariationRequest.ProductId))
        {
            return _modelsResolver.GetDaoFromDaoAndRequest(originalDao, updateProductVariationRequest);
        }
        else
        {
            var productDao = await _productsRepository.GetAsync(Guid.Parse(updateProductVariationRequest.ProductId));
            return _modelsResolver.GetDaoFromDaoAndRequest(originalDao, updateProductVariationRequest, productDao);
        }
    }
}
