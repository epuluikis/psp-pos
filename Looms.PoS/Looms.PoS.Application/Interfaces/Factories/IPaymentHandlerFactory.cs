using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Interfaces.Factories;

public interface IPaymentHandlerFactory
{
    IPaymentHandler GetHandler(PaymentMethod paymentMethod);
}
