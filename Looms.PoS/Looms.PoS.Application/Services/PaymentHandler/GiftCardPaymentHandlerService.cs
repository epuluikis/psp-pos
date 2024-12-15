using Looms.PoS.Application.Interfaces.Factories;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Application.Models.Responses.Payment;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Services.PaymentHandler;

public class GiftCardPaymentHandlerService : IPaymentHandlerService
{
    private readonly IPaymentsRepository _paymentsRepository;
    private readonly IGiftCardsRepository _giftCardsRepository;
    private readonly IPaymentModelsResolver _paymentModelsResolver;
    private readonly IGiftCardModelsResolver _giftCardModelsResolver;

    public PaymentMethod SupportedMethod => PaymentMethod.GiftCard;

    public GiftCardPaymentHandlerService(
        IPaymentsRepository paymentsRepository,
        IGiftCardsRepository giftCardsRepository,
        IPaymentModelsResolver paymentModelsResolver,
        IGiftCardModelsResolver giftCardModelsResolver
    )
    {
        _paymentsRepository = paymentsRepository;
        _giftCardsRepository = giftCardsRepository;
        _paymentModelsResolver = paymentModelsResolver;
        _giftCardModelsResolver = giftCardModelsResolver;
    }

    public async Task<PaymentResponse> HandlePayment(PaymentDao paymentDao)
    {
        var giftCardDao = await _giftCardsRepository.GetAsync(paymentDao.GiftCardId.GetValueOrDefault());

        if (!giftCardDao.IsActive)
        {
            throw new LoomsBadRequestException("Gift card is not active");
        }

        if (giftCardDao.ExpiryDate < DateTime.UtcNow)
        {
            throw new LoomsBadRequestException("Gift card is expired");
        }

        var currentBalance = giftCardDao.CurrentBalance;

        currentBalance -= paymentDao.Amount;
        currentBalance -= paymentDao.Tip;

        if (currentBalance < 0)
        {
            throw new LoomsBadRequestException("Gift card balance is insufficient");
        }

        giftCardDao = _giftCardModelsResolver.GetDaoFromDaoAndCurrentBalance(giftCardDao, currentBalance);
        await _giftCardsRepository.UpdateAsync(giftCardDao);

        paymentDao = _paymentModelsResolver.GetDaoFromDaoAndStatus(paymentDao, PaymentStatus.Succeeded);
        paymentDao = await _paymentsRepository.CreateAsync(paymentDao);

        return _paymentModelsResolver.GetResponseFromDao(paymentDao);
    }
}
