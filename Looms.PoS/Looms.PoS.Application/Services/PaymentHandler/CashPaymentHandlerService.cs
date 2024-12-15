using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Application.Models.Responses.Payment;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Services.PaymentHandler;

public class CashPaymentHandlerService : IPaymentHandlerService
{
    private readonly IPaymentsRepository _paymentsRepository;
    private readonly IPaymentModelsResolver _paymentModelsResolver;

    public PaymentMethod SupportedMethod => PaymentMethod.Cash;

    public CashPaymentHandlerService(
        IPaymentsRepository paymentsRepository,
        IPaymentModelsResolver paymentModelsResolver
    )
    {
        _paymentsRepository = paymentsRepository;
        _paymentModelsResolver = paymentModelsResolver;
    }

    public async Task<PaymentResponse> HandlePayment(PaymentDao paymentDao)
    {
        paymentDao = _paymentModelsResolver.GetDaoFromDaoAndStatus(paymentDao, PaymentStatus.Succeeded);
        paymentDao = await _paymentsRepository.CreateAsync(paymentDao);

        return _paymentModelsResolver.GetResponseFromDao(paymentDao);
    }
}
