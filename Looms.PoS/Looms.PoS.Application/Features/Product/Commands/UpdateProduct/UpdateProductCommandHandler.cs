﻿using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Product;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Product.Commands.UpdateProduct;

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
        var productRequest = await _httpContentResolver.GetPayloadAsync<CreateProductRequest>(command.Request);

        var productDao = _modelsResolver.GetDaoFromRequest(productRequest);
        var createdProductDao = await _productsRepository.CreateAsync(productDao);

        var response = _modelsResolver.GetResponseFromDao(createdProductDao);

        return new CreatedAtRouteResult($"/products{productDao.Id}", response);
    }
}