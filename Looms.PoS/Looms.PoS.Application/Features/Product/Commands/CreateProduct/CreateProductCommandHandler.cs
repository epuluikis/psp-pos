﻿using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Product;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace Looms.PoS.Application.Features.Product.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, IActionResult>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IProductModelsResolver _modelsResolver;

    public CreateProductCommandHandler(
        IProductsRepository productsRepository,
        IHttpContentResolver httpContentResolver,
        IProductModelsResolver modelsResolver)
    {
        _productsRepository = productsRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var productRequest = await _httpContentResolver.GetPayloadAsync<CreateProductRequest>(command.Request);

        var createdProductDao = _modelsResolver.GetDaoFromRequest(productRequest);

        var response = _modelsResolver.GetResponseFromDao(createdProductDao);

        return new CreatedAtRouteResult($"/products{createdProductDao.Id}", response);
    }
}
