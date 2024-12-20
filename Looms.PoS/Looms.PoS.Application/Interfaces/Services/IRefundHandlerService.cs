using Looms.PoS.Application.Models.Responses.Payment;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Interfaces.Services;

public interface IRefundHandlerService
{
    PaymentMethod SupportedMethod { get; }

    Task<RefundDao> HandleRefund(RefundDao refundDao);
}
