using Looms.PoS.Application.Interfaces.Factories;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Payment.Handlers;

public class CashPaymentHandler : IPaymentHandler
{
    private readonly IPaymentsRepository _paymentsRepository;
    private readonly IPaymentModelsResolver _modelsResolver;

    public PaymentMethod SupportedMethod => PaymentMethod.Cash;

    public CashPaymentHandler(
        IPaymentsRepository paymentsRepository,
        IPaymentModelsResolver modelsResolver
    )
    {
        _paymentsRepository = paymentsRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<PaymentResponse> HandlePayment(PaymentDao paymentDao)
    {
        paymentDao = await _paymentsRepository.CreateAsync(paymentDao);

        return _modelsResolver.GetResponseFromDao(paymentDao);
    }
}
