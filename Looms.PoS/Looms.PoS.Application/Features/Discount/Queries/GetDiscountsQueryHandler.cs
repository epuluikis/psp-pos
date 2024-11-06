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
    public Task<IActionResult> Handle(GetDiscountsQuery request, CancellationToken cancellationToken)
    {
        var discountDaos = _discountsRepository.GetAll().ToList();
        var response = _modelsResolver.GetResponseFromDao(discountDaos);

        return Task.FromResult<IActionResult>(new OkObjectResult(response));
    }
}
