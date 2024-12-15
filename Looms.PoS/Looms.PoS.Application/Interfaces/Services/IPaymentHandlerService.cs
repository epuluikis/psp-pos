using Looms.PoS.Application.Models.Responses.Payment;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Interfaces.Services;

public interface IPaymentHandlerService
{
    PaymentMethod SupportedMethod { get; }

    Task<PaymentResponse> HandlePayment(PaymentDao paymentDao);
}
