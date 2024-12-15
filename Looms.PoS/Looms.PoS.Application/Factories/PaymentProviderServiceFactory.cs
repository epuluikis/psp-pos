using Looms.PoS.Application.Interfaces.Factories;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Factories;

public class PaymentProviderServiceFactory : IPaymentProviderServiceFactory
{
    private readonly Dictionary<PaymentProviderType, IPaymentProviderService> _services;

    public PaymentProviderServiceFactory(IEnumerable<IPaymentProviderService> services)
    {
        _services = services.ToDictionary(h => h.Type, h => h);
    }

    public IPaymentProviderService GetService(PaymentProviderType type)
    {
        return _services[type];
    }
}
