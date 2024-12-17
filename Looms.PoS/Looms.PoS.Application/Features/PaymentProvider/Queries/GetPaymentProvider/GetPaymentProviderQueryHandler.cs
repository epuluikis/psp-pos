using Looms.PoS.Application.Helpers;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.PaymentProvider.Queries.GetPaymentProvider;

public class GetPaymentProviderQueryHandler : IRequestHandler<GetPaymentProviderQuery, IActionResult>
{
    private readonly IPaymentProvidersRepository _paymentProvidersRepository;
    private readonly IPaymentProviderModelsResolver _modelsResolver;

    public GetPaymentProviderQueryHandler(
        IPaymentProvidersRepository paymentProvidersRepository,
        IPaymentProviderModelsResolver modelsResolver)
    {
        _paymentProvidersRepository = paymentProvidersRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetPaymentProviderQuery request, CancellationToken cancellationToken)
    {
        var paymentProviderDao = await _paymentProvidersRepository.GetAsyncByIdAndBusinessId(
            Guid.Parse(request.Id),
            Guid.Parse(HttpContextHelper.GetHeaderBusinessId(request.Request))
        );

        var response = _modelsResolver.GetResponseFromDao(paymentProviderDao);

        return new OkObjectResult(response);
    }
}
