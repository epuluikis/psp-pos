using AutoMapper;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.ModelsResolvers;

public class RefundModelsResolver : IRefundModelsResolver
{
    private readonly IMapper _mapper;
    public RefundModelsResolver(IMapper mapper)
    {
        _mapper = mapper;
    }
    public RefundDao GetDaoFromRequest(CreateRefundRequest createRefundRequest)
        => _mapper.Map<RefundDao>(createRefundRequest);

    public RefundResponse GetResponseFromDao(RefundDao refundDao)
        => _mapper.Map<RefundResponse>(refundDao);

    public IEnumerable<RefundResponse> GetResponseFromDao(IEnumerable<RefundDao> refundDao)
        => _mapper.Map<IEnumerable<RefundResponse>>(refundDao);
}