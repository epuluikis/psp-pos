using Looms.PoS.Application.Models.Requests.Payment;
using Looms.PoS.Application.Models.Responses.Payment;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;

public interface IPaymentModelsResolver
{
    PaymentDao GetDaoFromRequest(CreatePaymentRequest createPaymentRequest);
    PaymentDao GetDaoFromDaoAndStatus(PaymentDao originalDao, PaymentStatus status);
    PaymentDao GetDaoFromDaoAndExternalId(PaymentDao originalDao, string externalId);
    PaymentResponse GetResponseFromDao(PaymentDao paymentDao);
    IEnumerable<PaymentResponse> GetResponseFromDao(IEnumerable<PaymentDao> paymentDao);
}
