using Looms.PoS.Application.Models.Requests.PaymentTerminal;
using Looms.PoS.Application.Models.Responses.PaymentTerminal;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;

public interface IPaymentTerminalModelsResolver
{
    PaymentTerminalDao GetDaoFromRequest(CreatePaymentTerminalRequest createPaymentTerminalRequest);
    PaymentTerminalDao GetDaoFromDaoAndRequest(PaymentTerminalDao originalDao, UpdatePaymentTerminalRequest updatePaymentTerminalRequest);
    PaymentTerminalDao GetDeletedDao(PaymentTerminalDao originalDao);
    PaymentTerminalResponse GetResponseFromDao(PaymentTerminalDao paymentTerminalDao);
    IEnumerable<PaymentTerminalResponse> GetResponseFromDao(IEnumerable<PaymentTerminalDao> paymentTerminalDao);
}
