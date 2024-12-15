using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Looms.PoS.Application.Interfaces.Services;

public interface IPaymentProviderService
{
    PaymentProviderType Type { get; }

    Task<bool> Validate(string externalId, string apiSecret);
    Task<bool> ValidateTerminal(PaymentProviderDao paymentProviderDao, string externalId);
    Task<PaymentDao> HandlePayment(PaymentDao paymentDao, PaymentProviderDao paymentProviderDao, PaymentTerminalDao paymentTerminalDao);
    Task HandleWebhook(HttpRequest request, PaymentProviderDao paymentProviderDao);
}
