using Looms.PoS.Application.Interfaces.Factories;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Features.Payment.Handlers;

public class PaymentHandlerFactory : IPaymentHandlerFactory
{
    private readonly Dictionary<PaymentMethod, IPaymentHandler> _handlers;

    public PaymentHandlerFactory(IEnumerable<IPaymentHandler> handlers)
    {
        _handlers = handlers.ToDictionary(h => h.SupportedMethod, h => h);
    }

    public IPaymentHandler GetHandler(PaymentMethod paymentMethod)
    {
        return _handlers[paymentMethod];
    }
}
