using Looms.PoS.Application.Helpers;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Refund.Queries.GetRefunds;

public class GetRefundsQueryHandler : IRequestHandler<GetRefundsQuery, IActionResult>
{
    private readonly IRefundsRepository _refundsRepository;
    private readonly IRefundModelsResolver _modelsResolver;

    public GetRefundsQueryHandler(IRefundsRepository refundsRepository, IRefundModelsResolver modelsResolver)
    {
        _refundsRepository = refundsRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetRefundsQuery query, CancellationToken cancellationToken)
    {
        var refundDaos = await _refundsRepository.GetAllAsyncByBusinessId(
            Guid.Parse(HttpContextHelper.GetHeaderBusinessId(query.Request))
        );

        var response = _modelsResolver.GetResponseFromDao(refundDaos);

        return new OkObjectResult(response);
    }
}
