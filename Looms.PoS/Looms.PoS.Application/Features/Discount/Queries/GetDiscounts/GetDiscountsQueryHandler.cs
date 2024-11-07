using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Discount.Queries;
public class GetDiscountsQueryHandler : IRequestHandler<GetDiscountsQuery, IActionResult>
{
    private readonly IDiscountsRepository _discountsRepository;
    private readonly IDiscountModelsResolver _modelsResolver;
    public GetDiscountsQueryHandler(IDiscountsRepository discountsRepository, IDiscountModelsResolver modelsResolver)
    {
        _discountsRepository = discountsRepository;
        _modelsResolver = modelsResolver;
    }
    public async Task<IActionResult> Handle(GetDiscountsQuery request, CancellationToken cancellationToken)
    {
        var discountDaos = await _discountsRepository.GetAllAsync();
        var response = _modelsResolver.GetResponseFromDao(discountDaos);

        return new OkObjectResult(response);
    }
}
