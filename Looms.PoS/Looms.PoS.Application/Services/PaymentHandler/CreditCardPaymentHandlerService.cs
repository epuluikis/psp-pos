using Looms.PoS.Application.Interfaces.Factories;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Application.Models.Responses.Payment;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Looms.PoS.Application.Services.PaymentHandler;

public class CreditCardPaymentHandlerService : IPaymentHandlerService
{
    private readonly IPaymentsRepository _paymentsRepository;
    private readonly IPaymentModelsResolver _paymentModelsResolver;
    private readonly IPaymentProviderServiceFactory _paymentProviderServiceFactory;
    private readonly IPaymentTerminalsRepository _paymentTerminalsRepository;

    public PaymentMethod SupportedMethod => PaymentMethod.CreditCard;

    public CreditCardPaymentHandlerService(
        IPaymentsRepository paymentsRepository,
        IPaymentModelsResolver paymentModelsResolver,
        IPaymentProviderServiceFactory paymentProviderServiceFactory,
        IPaymentTerminalsRepository paymentTerminalsRepository
    )
    {
        _paymentsRepository = paymentsRepository;
        _paymentModelsResolver = paymentModelsResolver;
        _paymentProviderServiceFactory = paymentProviderServiceFactory;
        _paymentTerminalsRepository = paymentTerminalsRepository;
    }

    public async Task<PaymentResponse> HandlePayment(PaymentDao paymentDao)
    {
        var paymentTerminalDao = await _paymentTerminalsRepository.GetAsync(paymentDao.PaymentTerminalId.GetValueOrDefault());

        paymentDao = await _paymentProviderServiceFactory
                           .GetService(paymentTerminalDao.PaymentProvider!.Type)
                           .HandlePayment(paymentDao, paymentTerminalDao.PaymentProvider, paymentTerminalDao);

        paymentDao = await _paymentsRepository.CreateAsync(paymentDao);

        return _paymentModelsResolver.GetResponseFromDao(paymentDao);
    }
}
