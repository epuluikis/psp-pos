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

    public async Task<IActionResult> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
    {
        var paymentDao = await _paymentsRepository.GetAsync(Guid.Parse(request.Id));

        var response = _modelsResolver.GetResponseFromDao(paymentDao);

        return new OkObjectResult(response);
    }
}
