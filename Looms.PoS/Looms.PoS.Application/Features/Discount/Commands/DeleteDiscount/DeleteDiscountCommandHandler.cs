using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Discount.Commands.DeleteDiscount;

public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, IActionResult>
{
    private readonly IDiscountsRepository _discountsRepository;
    private readonly IDiscountModelsResolver _modelsResolver;

    public DeleteDiscountCommandHandler(IDiscountsRepository discountsRepository, IDiscountModelsResolver modelsResolver)
    {
        _discountsRepository = discountsRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
    {
        var originalDao = await _discountsRepository.GetAsync(Guid.Parse(request.Id));

        var orderDao = _modelsResolver.GetDeletedDao(originalDao);
        _ = await _discountsRepository.UpdateAsync(orderDao);

        return new NoContentResult();
    }
}
