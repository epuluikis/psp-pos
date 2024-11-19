using Looms.PoS.Application.Features.Refund.Queries.GetRefund;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Discount.Queries.GetDiscount;

public class GetRefundsQueryHandler : IRequestHandler<GetRefundQuery, IActionResult>
{
    private readonly IRefundsRepository _refundsRepository;
    private readonly IRefundModelsResolver _modelsResolver;

    public GetRefundsQueryHandler(IRefundsRepository refundsRepository, IRefundModelsResolver modelsResolver)
    {
        _refundsRepository = refundsRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetRefundQuery request, CancellationToken cancellationToken)
    {
        var businessDao = await _refundsRepository.GetAsync(Guid.Parse(request.Id));

        var response = _modelsResolver.GetResponseFromDao(businessDao);

        return new OkObjectResult(response);
    }
}
