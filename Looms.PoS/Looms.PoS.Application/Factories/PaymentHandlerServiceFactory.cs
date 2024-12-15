using Looms.PoS.Application.Interfaces.Factories;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Factories;

public class PaymentHandlerServiceFactory : IPaymentHandlerServiceFactory
{
    private readonly Dictionary<PaymentMethod, IPaymentHandlerService> _services;

    public PaymentHandlerServiceFactory(IEnumerable<IPaymentHandlerService> services)
    {
        _services = services.ToDictionary(h => h.SupportedMethod, h => h);
    }

    public IPaymentHandlerService GetService(PaymentMethod paymentMethod)
    {
        return _services[paymentMethod];
    }
}
