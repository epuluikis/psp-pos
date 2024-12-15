using AutoMapper;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.PaymentProvider;
using Looms.PoS.Application.Models.Responses.PaymentProvider;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.ModelsResolvers;

public class PaymentProviderModelsResolver : IPaymentProviderModelsResolver
{
    private readonly IMapper _mapper;

    public PaymentProviderModelsResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public PaymentProviderDao GetDaoFromRequest(CreatePaymentProviderRequest createPaymentProviderRequest)
    {
        return _mapper.Map<PaymentProviderDao>(createPaymentProviderRequest);
    }

    public PaymentProviderDao GetDaoFromDaoAndRequest(
        PaymentProviderDao originalDao,
        UpdatePaymentProviderRequest updatePaymentProviderRequest)
    {
        return _mapper.Map<PaymentProviderDao>(updatePaymentProviderRequest) with { Id = originalDao.Id, IsDeleted = originalDao.IsDeleted };
    }

    public PaymentProviderDao GetDeletedDao(PaymentProviderDao originalDao)
    {
        return originalDao with { IsDeleted = true };
    }

    public PaymentProviderResponse GetResponseFromDao(PaymentProviderDao paymentProviderDao)
    {
        return _mapper.Map<PaymentProviderResponse>(paymentProviderDao);
    }

    public IEnumerable<PaymentProviderResponse> GetResponseFromDao(IEnumerable<PaymentProviderDao> paymentProviderDao)
    {
        return _mapper.Map<IEnumerable<PaymentProviderResponse>>(paymentProviderDao);
    }
}
