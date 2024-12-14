using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.PaymentProvider;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.PaymentProvider.Commands.UpdatePaymentProvider;

public class UpdatePaymentProviderCommandHandler : IRequestHandler<UpdatePaymentProviderCommand, IActionResult>
{
    private readonly IPaymentProvidersRepository _paymentProvidersRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IPaymentProviderModelsResolver _modelsResolver;

    public UpdatePaymentProviderCommandHandler(
        IPaymentProvidersRepository paymentProvidersRepository,
        IHttpContentResolver httpContentResolver,
        IPaymentProviderModelsResolver modelsResolver)
    {
        _paymentProvidersRepository = paymentProvidersRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(UpdatePaymentProviderCommand command, CancellationToken cancellationToken)
    {
        var updatePaymentProviderRequest = await _httpContentResolver.GetPayloadAsync<UpdatePaymentProviderRequest>(command.Request);

        var originalDao = await _paymentProvidersRepository.GetAsync(Guid.Parse(command.Id));

        var paymentProviderDao = _modelsResolver.GetDaoFromDaoAndRequest(originalDao, updatePaymentProviderRequest);
        var updatedPaymentProviderDao = await _paymentProvidersRepository.UpdateAsync(paymentProviderDao);

        var response = _modelsResolver.GetResponseFromDao(updatedPaymentProviderDao);

        return new OkObjectResult(response);
    }
}
