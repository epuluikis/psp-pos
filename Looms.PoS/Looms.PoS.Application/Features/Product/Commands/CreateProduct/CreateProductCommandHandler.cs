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
    private readonly IBusinessesRepository _businessesRepository;
    public CreateProductCommandHandler(
        IProductsRepository productsRepository,
        IHttpContentResolver httpContentResolver,
        IProductModelsResolver modelsResolver,
        ITaxesRepository taxesRepository,
        IBusinessesRepository businessesRepository)
    {
        _productsRepository = productsRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
        _taxesRepository = taxesRepository;
        _businessesRepository = businessesRepository;
    }

    public async Task<IActionResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var productRequest = await _httpContentResolver.GetPayloadAsync<CreateProductRequest>(command.Request);

        var businessId = command.Request.Headers["BusinessId"];
        var business = await _businessesRepository.GetAsync(Guid.Parse(businessId));

        TaxDao tax = await GetTaxAsync(productRequest.TaxId);
        
        var productDao = _modelsResolver.GetDaoFromRequest(productRequest, business, tax);
        var createdProduct = await _productsRepository.CreateAsync(productDao);
        var response = _modelsResolver.GetResponseFromDao(createdProduct);

        return new CreatedAtRouteResult($"/products/{productDao.Id}", response);
    }

    private async Task<TaxDao> GetTaxAsync(string? taxId)
    {
        if (taxId is null)
        {
            return await _taxesRepository.GetByTaxCategoryAsync(TaxCategory.Product);
        }
        else
        {
            return await _taxesRepository.GetAsync(Guid.Parse(taxId));
        }
    }
}
