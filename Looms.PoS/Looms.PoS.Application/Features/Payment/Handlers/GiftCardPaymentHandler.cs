using Looms.PoS.Application.Interfaces.Factories;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Responses.Payment;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Payment.Handlers;

public class GiftCardPaymentHandler : IPaymentHandler
{
    private readonly IPaymentsRepository _paymentsRepository;
    private readonly IGiftCardsRepository _giftCardsRepository;
    private readonly IPaymentModelsResolver _modelsResolver;

    public PaymentMethod SupportedMethod => PaymentMethod.GiftCard;

    public GiftCardPaymentHandler(
        IPaymentsRepository paymentsRepository,
        IGiftCardsRepository giftCardsRepository,
        IPaymentModelsResolver modelsResolver
    )
    {
        _paymentsRepository = paymentsRepository;
        _giftCardsRepository = giftCardsRepository;
        _modelsResolver = modelsResolver;
    }

    public async Task<PaymentResponse> HandlePayment(PaymentDao paymentDao)
    {
        var giftCardDao = await _giftCardsRepository.GetAsync(paymentDao.GiftCardId.GetValueOrDefault());

        if (!giftCardDao.IsActive)
        {
            throw new LoomsBadRequestException("Gift card is not active");
        }

        if (giftCardDao.ExpiryDate < DateTime.Now)
        {
            throw new LoomsBadRequestException("Gift card is expired");
        }

        giftCardDao.CurrentBalance -= paymentDao.Amount;
        giftCardDao.CurrentBalance -= paymentDao.Tip;

        if (giftCardDao.CurrentBalance < 0)
        {
            throw new LoomsBadRequestException("Gift card balance is insufficient");
        }

        await _giftCardsRepository.Save();

        paymentDao = await _paymentsRepository.CreateAsync(paymentDao);

        return _modelsResolver.GetResponseFromDao(paymentDao);
    }
}
