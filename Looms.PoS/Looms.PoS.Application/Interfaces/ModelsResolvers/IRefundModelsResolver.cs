using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Requests.Refund;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Application.Models.Responses.Refund;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;

public interface IRefundModelsResolver
{
    RefundDao GetDaoFromRequest(CreateRefundRequest createRefundRequest, Guid userId, PaymentDao paymentDao);
    RefundResponse GetResponseFromDao(RefundDao refundDao);
    IEnumerable<RefundResponse> GetResponseFromDao(IEnumerable<RefundDao> refundDao);
    RefundDao GetDaoFromDaoAndStatus(RefundDao refundDao, RefundStatus status);
    RefundDao GetDaoFromDaoAndExternalId(RefundDao originalDao, string externalId);
}
