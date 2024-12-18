using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Services.RefundHandler;

public class CashRefundHandlerService : IRefundHandlerService
{
    private readonly IRefundsRepository _refundsRepository;
    private readonly IRefundModelsResolver _refundModelsResolver;

    public PaymentMethod SupportedMethod => PaymentMethod.Cash;

    public CashRefundHandlerService(
        IRefundsRepository refundsRepository,
        IRefundModelsResolver refundModelsResolver
    )
    {
        _refundsRepository = refundsRepository;
        _refundModelsResolver = refundModelsResolver;
    }

    public async Task<RefundDao> HandleRefund(RefundDao refundDao)
    {
        refundDao = _refundModelsResolver.GetDaoFromDaoAndStatus(refundDao, RefundStatus.Completed);

        return await _refundsRepository.CreateAsync(refundDao);
    }
}
