using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Models.Responses;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Looms.PoS.Application.Features.Payment.Commands.CreatePayment;

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, IActionResult>
{
    private readonly IPaymentsRepository _paymentsRepository;
    private readonly IGiftCardsRepository _giftCardsRepository;
    private readonly IHttpContentResolver _httpContentResolver;
    private readonly IPaymentModelsResolver _modelsResolver;

    public CreatePaymentCommandHandler(
        IPaymentsRepository paymentsRepository,
        IGiftCardsRepository giftCardsRepository,
        IHttpContentResolver httpContentResolver,
        IPaymentModelsResolver modelsResolver)
    {
        _paymentsRepository = paymentsRepository;
        _giftCardsRepository = giftCardsRepository;
        _httpContentResolver = httpContentResolver;
        _modelsResolver = modelsResolver;
    }

    public async Task<IActionResult> Handle(CreatePaymentCommand command, CancellationToken cancellationToken)
    {
        CreatePaymentRequest paymentRequest = await _httpContentResolver.GetPayloadAsync<CreatePaymentRequest>(command.Request);

        PaymentDao paymentDao = _modelsResolver.GetDaoFromRequest(paymentRequest);

        PaymentResponse response = paymentDao.PaymentMethod switch
        {
            PaymentMethod.Cash => await HandleCash(paymentDao),
            PaymentMethod.CreditCard => await HandleCreditCard(paymentDao),
            PaymentMethod.GiftCard => await HandleGiftCard(paymentDao),
            _ => throw new Exception($"Unknown payment method type: {paymentRequest.PaymentMethod}")
        };

        return new CreatedAtRouteResult($"/payments/{paymentDao.Id}", response);
    }

    private async Task<PaymentResponse> HandleCash(PaymentDao paymentDao)
    {
        paymentDao = await _paymentsRepository.CreateAsync(paymentDao);

        return _modelsResolver.GetResponseFromDao(paymentDao);
    }

    private async Task<PaymentResponse> HandleCreditCard(PaymentDao paymentDao)
    {
        throw new NotImplementedException();
    }

    // TODO: check gift card -> business -> order -> payment relation
    private async Task<PaymentResponse> HandleGiftCard(PaymentDao paymentDao)
    {
        GiftCardDao giftCardDao = await _giftCardsRepository.GetAsync(paymentDao.GiftCardId.GetValueOrDefault());

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
