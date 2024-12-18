using Looms.PoS.Application.Interfaces.Factories;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Factories;

public class RefundHandlerServiceFactory : IRefundHandlerServiceFactory
{
    private readonly Dictionary<PaymentMethod, IRefundHandlerService> _services;

    public RefundHandlerServiceFactory(IEnumerable<IRefundHandlerService> services)
    {
        _services = services.ToDictionary(h => h.SupportedMethod, h => h);
    }

    public IRefundHandlerService GetService(PaymentMethod paymentMethod)
    {
        return _services[paymentMethod];
    }
}
