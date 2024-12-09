﻿using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Product.Commands.DeleteProductVariation;

public class DeleteProductVariationCommandHandler : IRequestHandler<DeleteProductVariationCommand, IActionResult>
{
    private readonly IProductVariationRepository _productVariationRepository;
    private readonly IProductVariationModelsResolver _modelsResolver;

    public DeleteProductVariationCommandHandler(
        IProductVariationRepository productVariationRepository,
        IProductVariationModelsResolver modelsResolver)
    {
        _productVariationRepository = productVariationRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(DeleteProductVariationCommand command, CancellationToken cancellationToken)
    {
        var originalDao = await _productVariationRepository.GetAsync(Guid.Parse(command.Id));

        var productVariationDao = _modelsResolver.GetDeletedDao(originalDao);
        _ = await _productVariationRepository.UpdateAsync(productVariationDao);

        return new NoContentResult();
    }
}
