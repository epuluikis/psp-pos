using AutoMapper;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Refund;
using Looms.PoS.Application.Models.Responses.Refund;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Mappings.ModelsResolvers;

public class RefundModelsResolver : IRefundModelsResolver
{
    private readonly IMapper _mapper;

    public RefundModelsResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public RefundDao GetDaoFromRequest(CreateRefundRequest createRefundRequest, Guid userId, PaymentDao paymentDao)
    {
        return _mapper.Map<RefundDao>(createRefundRequest) with { UserId = userId, Payment = paymentDao };
    }

    public RefundResponse GetResponseFromDao(RefundDao refundDao)
    {
        return _mapper.Map<RefundResponse>(refundDao);
    }

    public IEnumerable<RefundResponse> GetResponseFromDao(IEnumerable<RefundDao> refundDao)
    {
        return _mapper.Map<IEnumerable<RefundResponse>>(refundDao);
    }

    public RefundDao GetDaoFromDaoAndStatus(RefundDao refundDao, RefundStatus status)
    {
        return _mapper.Map<RefundDao>(refundDao) with { Status = status };
    }

    public RefundDao GetDaoFromDaoAndExternalId(RefundDao originalDao, string externalId)
    {
        return _mapper.Map<RefundDao>(originalDao) with { ExternalId = externalId };
    }
}
