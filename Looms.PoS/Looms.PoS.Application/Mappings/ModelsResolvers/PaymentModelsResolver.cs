using AutoMapper;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Payment;
using Looms.PoS.Application.Models.Responses.Payment;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Mappings.ModelsResolvers;

public class PaymentModelsResolver : IPaymentModelsResolver
{
    private readonly IMapper _mapper;

    public PaymentModelsResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public PaymentDao GetDaoFromRequest(CreatePaymentRequest createPaymentRequest)
    {
        return _mapper.Map<PaymentDao>(createPaymentRequest);
    }

    public PaymentDao GetDaoFromDaoAndStatus(PaymentDao originalDao, PaymentStatus status)
    {
        return _mapper.Map<PaymentDao>(originalDao) with { Status = status };
    }

    public PaymentDao GetDaoFromDaoAndExternalId(PaymentDao originalDao, string externalId)
    {
        return _mapper.Map<PaymentDao>(originalDao) with { ExternalId = externalId };
    }

    public PaymentResponse GetResponseFromDao(PaymentDao paymentDao)
    {
        return _mapper.Map<PaymentResponse>(paymentDao);
    }

    public IEnumerable<PaymentResponse> GetResponseFromDao(IEnumerable<PaymentDao> paymentDao)
    {
        return _mapper.Map<IEnumerable<PaymentResponse>>(paymentDao);
    }
}
