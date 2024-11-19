﻿using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Product.Commands.DeleteProduct;
//TODO: Should this delete associated ProductVariation and ProductStock entries?
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, IActionResult>
{
    private readonly IProductsRepository _productsRepository;
    private readonly IProductModelsResolver _modelsResolver;

    public DeleteProductCommandHandler(
        IProductsRepository productsRepository,
        IProductModelsResolver modelsResolver)
    {
        _productsRepository = productsRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var originalDao = await _productsRepository.GetAsync(Guid.Parse(command.Id));

        var productDao = _modelsResolver.GetDeletedDao(originalDao);
        _ = await _productsRepository.UpdateAsync(productDao);

        return new NoContentResult();
    }
}
