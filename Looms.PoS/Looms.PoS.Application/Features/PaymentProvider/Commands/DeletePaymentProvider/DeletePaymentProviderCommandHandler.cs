using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.PaymentProvider.Commands.DeletePaymentProvider;

public class DeletePaymentProviderCommandHandler : IRequestHandler<DeletePaymentProviderCommand, IActionResult>
{
    private readonly IPaymentProvidersRepository _paymentProvidersRepository;
    private readonly IPaymentProviderModelsResolver _modelsResolver;

    public DeletePaymentProviderCommandHandler(
        IPaymentProvidersRepository paymentProvidersRepository,
        IPaymentProviderModelsResolver modelsResolver)
    {
        _paymentProvidersRepository = paymentProvidersRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(DeletePaymentProviderCommand command, CancellationToken cancellationToken)
    {
        var originalDao = await _paymentProvidersRepository.GetAsync(Guid.Parse(command.Id));

        var paymentProviderDao = _modelsResolver.GetDeletedDao(originalDao);
        _ = await _paymentProvidersRepository.UpdateAsync(paymentProviderDao);

        return new NoContentResult();
    }
}
