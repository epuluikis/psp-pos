using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Interfaces.Factories;

public interface IPaymentProviderServiceFactory
{
    IPaymentProviderService GetService(PaymentProviderType type);
}
