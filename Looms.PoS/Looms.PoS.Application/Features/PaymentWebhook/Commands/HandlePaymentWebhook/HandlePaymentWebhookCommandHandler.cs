using Looms.PoS.Application.Features.PaymentProvider.Commands.CreatePaymentProvider;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.Factories;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.PaymentProvider;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.PaymentWebhook.Commands.HandlePaymentWebhook;

public class HandlePaymentWebhookCommandHandler : IRequestHandler<HandlePaymentWebhookCommand, IActionResult>
{
    private readonly IPaymentProvidersRepository _paymentProvidersRepository;
    private readonly IPaymentProviderServiceFactory _paymentProviderServiceFactory;

    public HandlePaymentWebhookCommandHandler(
        IPaymentProvidersRepository paymentProvidersRepository,
        IPaymentProviderServiceFactory paymentProviderServiceFactory
    )
    {
        _paymentProvidersRepository = paymentProvidersRepository;
        _paymentProviderServiceFactory = paymentProviderServiceFactory;
    }

    public async Task<IActionResult> Handle(HandlePaymentWebhookCommand command, CancellationToken cancellationToken)
    {
        var paymentProviderDao = await _paymentProvidersRepository.GetAsync(Guid.Parse(command.PaymentProviderId));

        await _paymentProviderServiceFactory.GetService(paymentProviderDao.Type).HandleWebhook(command.Request, paymentProviderDao);

        return new NoContentResult();
    }
}
