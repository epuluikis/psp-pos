using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using System.Transactions;

namespace Looms.PoS.Application.Services.RefundHandler;

public class GiftCardRefundHandlerService : IRefundHandlerService
{
    private readonly IGiftCardsRepository _giftCardsRepository;
    private readonly IGiftCardModelsResolver _giftCardModelsResolver;
    private readonly IRefundsRepository _refundsRepository;
    private readonly IRefundModelsResolver _refundModelsResolver;
    public PaymentMethod SupportedMethod => PaymentMethod.GiftCard;

    public GiftCardRefundHandlerService(
        IGiftCardsRepository giftCardsRepository,
        IGiftCardModelsResolver giftCardModelsResolver,
        IRefundsRepository refundsRepository,
        IRefundModelsResolver refundModelsResolver
    )
    {
        _giftCardsRepository = giftCardsRepository;
        _giftCardModelsResolver = giftCardModelsResolver;
        _refundsRepository = refundsRepository;
        _refundModelsResolver = refundModelsResolver;
    }

    public async Task<RefundDao> HandleRefund(RefundDao refundDao)
    {
        var giftCardDao = await _giftCardsRepository.GetAsync(refundDao.Payment.GiftCardId.GetValueOrDefault());

        giftCardDao = _giftCardModelsResolver.GetDaoFromDaoAndCurrentBalance(giftCardDao, giftCardDao.CurrentBalance + refundDao.Amount);
        refundDao = _refundModelsResolver.GetDaoFromDaoAndStatus(refundDao, RefundStatus.Completed);

        using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _giftCardsRepository.UpdateAsync(giftCardDao);
            refundDao = await _refundsRepository.CreateAsync(refundDao);

            transactionScope.Complete();
        }

        return refundDao;
    }
}
