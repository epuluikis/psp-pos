using Looms.PoS.Application.Models.Requests.PaymentProvider;
using Looms.PoS.Application.Models.Responses.PaymentProvider;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;

public interface IPaymentProviderModelsResolver
{
    PaymentProviderDao GetDaoFromRequest(CreatePaymentProviderRequest createPaymentProviderRequest, Guid businessId);
    PaymentProviderDao GetDaoFromDaoAndRequest(PaymentProviderDao originalDao, UpdatePaymentProviderRequest updatePaymentProviderRequest);
    PaymentProviderDao GetDeletedDao(PaymentProviderDao originalDao);
    PaymentProviderResponse GetResponseFromDao(PaymentProviderDao paymentProviderDao);
    IEnumerable<PaymentProviderResponse> GetResponseFromDao(IEnumerable<PaymentProviderDao> paymentProviderDao);
}
