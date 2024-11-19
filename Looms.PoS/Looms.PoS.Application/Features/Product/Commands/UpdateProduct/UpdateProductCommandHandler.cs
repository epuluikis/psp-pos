using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Product;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Product.Commands.UpdateProduct;
//TODO: include ProductVariation and ProductStock repos
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, IActionResult>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IProductModelsResolver _modelsResolver;

    public UpdateProductCommandHandler(
        IProductsRepository productsRepository,
        IHttpContentResolver httpContentResolver,
        IProductModelsResolver modelsResolver)
    {
        _productsRepository = productsRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var updateProductRequest = await _httpContentResolver.GetPayloadAsync<UpdateProductRequest>(command.Request);

        var originalDao = await _productsRepository.GetAsync(Guid.Parse(command.Id));

        var productDao = _modelsResolver.GetDaoFromDaoAndRequest(originalDao, updateProductRequest);
        var updatedProductDao = await _productsRepository.UpdateAsync(productDao);

        var response = _modelsResolver.GetResponseFromDao(updatedProductDao);

        return new OkObjectResult(response);
    }
}
