using Looms.PoS.Application.Interfaces.Factories;
using Looms.PoS.Application.Models.Responses.Payment;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Features.Payment.Handlers;

public class CreditCardPaymentHandler : IPaymentHandler
{
    public PaymentMethod SupportedMethod => PaymentMethod.CreditCard;

    public async Task<PaymentResponse> HandlePayment(PaymentDao paymentDao)
    {
        throw new NotImplementedException();
    }
}
