using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.PaymentProvider.Queries.GetPaymentProviders;

public class GetPaymentProvidersQueryHandler : IRequestHandler<GetPaymentProvidersQuery, IActionResult>
{
    private readonly IPaymentProvidersRepository _paymentProvidersRepository;
    private readonly IPaymentProviderModelsResolver _modelsResolver;

    public GetPaymentProvidersQueryHandler(
        IPaymentProvidersRepository paymentProvidersRepository,
        IPaymentProviderModelsResolver modelsResolver)
    {
        _paymentProvidersRepository = paymentProvidersRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(GetPaymentProvidersQuery request, CancellationToken cancellationToken)
    {
        var paymentProviderDaos = await _paymentProvidersRepository.GetAllAsync();

        var response = _modelsResolver.GetResponseFromDao(paymentProviderDaos);

        return new OkObjectResult(response);
    }
}
