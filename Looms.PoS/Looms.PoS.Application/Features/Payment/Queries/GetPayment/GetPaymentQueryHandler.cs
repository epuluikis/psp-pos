using Looms.PoS.Application.Helpers;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.Payment.Queries.GetPayment;

public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, IActionResult>
{
    private readonly IPaymentsRepository _paymentsRepository;
    private readonly IPaymentModelsResolver _modelsResolver;

    public GetPaymentQueryHandler(IPaymentsRepository paymentsRepository, IPaymentModelsResolver modelsResolver)
    {
        _paymentsRepository = paymentsRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetPaymentQuery query, CancellationToken cancellationToken)
    {
        var paymentDao = await _paymentsRepository.GetAsyncByIdAndBusinessId(
            Guid.Parse(query.Id),
            Guid.Parse(HttpContextHelper.GetHeaderBusinessId(query.Request))
        );

        var response = _modelsResolver.GetResponseFromDao(paymentDao);

        return new OkObjectResult(response);
    }
}
