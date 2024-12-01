using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Discount.Queries.GetDiscount;

public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, IActionResult>
{
    private readonly IDiscountsRepository _discountsRepository;
    private readonly IDiscountModelsResolver _modelsResolver;

    public GetDiscountQueryHandler(IDiscountsRepository discountsRepository, IDiscountModelsResolver modelsResolver)
    {
        _discountsRepository = discountsRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
    {
        var businessDao = await _discountsRepository.GetAsync(Guid.Parse(request.Id));

        var response = _modelsResolver.GetResponseFromDao(businessDao);

        return new OkObjectResult(response);
    }
}
