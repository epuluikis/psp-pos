using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Interfaces.Factories;

public interface IPaymentHandler
{
    PaymentMethod SupportedMethod { get; }

    Task<PaymentResponse> HandlePayment(PaymentDao paymentDao);
}
