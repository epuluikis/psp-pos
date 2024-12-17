using Looms.PoS.Application.Helpers;
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

    public async Task<IActionResult> Handle(GetDiscountQuery query, CancellationToken cancellationToken)
    {
        var businessDao = await _discountsRepository.GetAsyncByIdAndBusinessId(
            Guid.Parse(query.Id),
            Guid.Parse(HttpContextHelper.GetHeaderBusinessId(query.Request))
        );

        var response = _modelsResolver.GetResponseFromDao(businessDao);

        return new OkObjectResult(response);
    }
}
