using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Product;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Product.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, IActionResult>
{
    private readonly IProductsRepository _ProductsRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IProductModelsResolver _modelsResolver;

    public CreateProductCommandHandler(
        IProductsRepository ProductsRepository,
        IHttpContentResolver httpContentResolver,
        IProductModelsResolver modelsResolver)
    {
        _ProductsRepository = ProductsRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var ProductRequest = await _httpContentResolver.GetPayloadAsync<CreateProductRequest>(command.Request);

        var ProductDao = _modelsResolver.GetDaoFromRequest(ProductRequest);
        var createdProductDao = await _ProductsRepository.CreateAsync(ProductDao);

        var response = _modelsResolver.GetResponseFromDao(createdProductDao);

        return new CreatedAtRouteResult($"/Products{ProductDao.Id}", response);
    }
}
