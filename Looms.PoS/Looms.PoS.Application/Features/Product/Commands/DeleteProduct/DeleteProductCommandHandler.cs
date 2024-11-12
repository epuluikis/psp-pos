﻿using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Product.Commands.DeleteProduct;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, IActionResult>
{
    private readonly IProductsRepository _ProductsRepository;
    private readonly IProductModelsResolver _modelsResolver;

    public DeleteProductCommandHandler(
        IProductsRepository ProductsRepository,
        IProductModelsResolver modelsResolver)
    {
        _ProductsRepository = ProductsRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var originalDao = await _ProductsRepository.GetAsync(Guid.Parse(command.Id));

        var ProductDao = _modelsResolver.GetDeletedDao(originalDao);
        _ = await _ProductsRepository.UpdateAsync(ProductDao);

        return new NoContentResult();
    }
}
