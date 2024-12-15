using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.PaymentProvider;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.PaymentProvider.Commands.CreatePaymentProvider;

public class CreatePaymentProviderCommandHandler : IRequestHandler<CreatePaymentProviderCommand, IActionResult>
{
    private readonly IPaymentProvidersRepository _paymentProvidersRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IPaymentProviderModelsResolver _modelsResolver;

    public CreatePaymentProviderCommandHandler(
        IPaymentProvidersRepository paymentProvidersRepository,
        IHttpContentResolver httpContentResolver,
        IPaymentProviderModelsResolver modelsResolver)
    {
        _paymentProvidersRepository = paymentProvidersRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(CreatePaymentProviderCommand command, CancellationToken cancellationToken)
    {
        var paymentProviderRequest = await _httpContentResolver.GetPayloadAsync<CreatePaymentProviderRequest>(command.Request);

        var paymentProviderDao = _modelsResolver.GetDaoFromRequest(paymentProviderRequest);
        var createdPaymentProviderDao = await _paymentProvidersRepository.CreateAsync(paymentProviderDao);

        var response = _modelsResolver.GetResponseFromDao(createdPaymentProviderDao);

        return new CreatedAtRouteResult($"/paymentproviders/{paymentProviderDao.Id}", response);
    }
}
