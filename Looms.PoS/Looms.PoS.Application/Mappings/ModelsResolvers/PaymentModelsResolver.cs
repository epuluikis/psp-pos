using AutoMapper;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;

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

    public PaymentResponse GetResponseFromDao(PaymentDao paymentDao)
    {
        return _mapper.Map<PaymentResponse>(paymentDao);
    }

    public IEnumerable<PaymentResponse> GetResponseFromDao(IEnumerable<PaymentDao> paymentDao)
    {
        return _mapper.Map<IEnumerable<PaymentResponse>>(paymentDao);
    }
}
