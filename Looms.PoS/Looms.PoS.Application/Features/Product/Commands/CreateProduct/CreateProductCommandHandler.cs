using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Product;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Product.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, IActionResult>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IProductModelsResolver _modelsResolver;
    private readonly ITaxesRepository _taxesRepository;

    public CreateProductCommandHandler(
        IProductsRepository productsRepository,
        IHttpContentResolver httpContentResolver,
        IProductModelsResolver modelsResolver,
        ITaxesRepository taxesRepository)
    {
        _productsRepository = productsRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
        _taxesRepository = taxesRepository;
    }

    public async Task<IActionResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var productRequest = await _httpContentResolver.GetPayloadAsync<CreateProductRequest>(command.Request);

        ProductDao productDao;

        if (productRequest.TaxId is null)
        {
            var tax = await _taxesRepository.GetByTaxCategoryAsync(TaxCategory.Product);
            productDao = _modelsResolver.GetDaoFromRequest(productRequest, tax);
        }else{
            var tax = await _taxesRepository.GetAsync(Guid.Parse(productRequest.TaxId));
            productDao = _modelsResolver.GetDaoFromRequest(productRequest, tax);
        }
        var createdProductDao = await _productsRepository.CreateAsync(productDao);
        var response = _modelsResolver.GetResponseFromDao(createdProductDao);

        return new CreatedAtRouteResult($"/products/{createdProductDao.Id}", response);
    }
}
