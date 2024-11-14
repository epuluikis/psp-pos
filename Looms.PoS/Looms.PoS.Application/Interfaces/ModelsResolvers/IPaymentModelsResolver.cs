using Looms.PoS.Application.Models.Requests.Payment;
using Looms.PoS.Application.Models.Responses.Payment;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;

public interface IPaymentModelsResolver
{
    PaymentDao GetDaoFromRequest(CreatePaymentRequest createPaymentRequest);
    PaymentResponse GetResponseFromDao(PaymentDao paymentDao);
    IEnumerable<PaymentResponse> GetResponseFromDao(IEnumerable<PaymentDao> paymentDao);
}
