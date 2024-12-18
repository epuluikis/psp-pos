using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Interfaces.Factories;

public interface IRefundHandlerServiceFactory
{
    IRefundHandlerService GetService(PaymentMethod paymentMethod);
}
