using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;
public interface IRefundModelsResolver
{
    RefundDao GetDaoFromRequest(CreateRefundRequest createRefundRequest);
    RefundResponse GetResponseFromDao(RefundDao refundDao);
    IEnumerable<RefundResponse> GetResponseFromDao(IEnumerable<RefundDao> refundDao);
}
