using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Discount.Queries;
public class GetDiscountsQueryHandler : IRequestHandler<GetDiscountsQuery, IActionResult>
{
    private readonly IDiscountsRepository _discountsRepository;
    public GetDiscountsQueryHandler(IDiscountsRepository discountsRepository)
    {
        _discountsRepository = discountsRepository;
    }
    public async Task<IActionResult> Handle(GetDiscountsQuery request, CancellationToken cancellationToken)
    {
        await Task.Delay(10, cancellationToken);

        var businessDaos = _discountsRepository.GetAll();

        return new OkObjectResult(businessDaos);
    }
}
