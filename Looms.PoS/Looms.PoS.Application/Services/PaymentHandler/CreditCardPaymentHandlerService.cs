using Looms.PoS.Application.Interfaces.Factories;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Services.PaymentHandler;

public class CreditCardPaymentHandlerService : IPaymentHandlerService
{
    private readonly IPaymentsRepository _paymentsRepository;
    private readonly IPaymentProviderServiceFactory _paymentProviderServiceFactory;
    private readonly IPaymentTerminalsRepository _paymentTerminalsRepository;

    public PaymentMethod SupportedMethod => PaymentMethod.CreditCard;

    public CreditCardPaymentHandlerService(
        IPaymentsRepository paymentsRepository,
        IPaymentProviderServiceFactory paymentProviderServiceFactory,
        IPaymentTerminalsRepository paymentTerminalsRepository
    )
    {
        _paymentsRepository = paymentsRepository;
        _paymentProviderServiceFactory = paymentProviderServiceFactory;
        _paymentTerminalsRepository = paymentTerminalsRepository;
    }

    public async Task<PaymentDao> HandlePayment(PaymentDao paymentDao)
    {
        var paymentTerminalDao = await _paymentTerminalsRepository.GetAsync(paymentDao.PaymentTerminalId.GetValueOrDefault());

        paymentDao = await _paymentProviderServiceFactory
                           .GetService(paymentTerminalDao.PaymentProvider.Type)
                           .HandlePayment(paymentDao, paymentTerminalDao.PaymentProvider, paymentTerminalDao);

        return await _paymentsRepository.CreateAsync(paymentDao);
    }
}
