using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Stripe;
using Stripe.Terminal;
using System.Diagnostics;
using System.Linq;

namespace Looms.PoS.Application.Services.PaymentProvider;

public class StripePaymentProviderService : IPaymentProviderService
{
    private readonly string[] _paymentWebhookEventTypes =
    [
        EventTypes.PaymentIntentCanceled,
        EventTypes.PaymentIntentPaymentFailed,
        EventTypes.PaymentIntentProcessing,
        EventTypes.PaymentIntentRequiresAction,
        EventTypes.PaymentIntentSucceeded
    ];

    private readonly string[] _refundWebhookEventTypes =
    [
        EventTypes.RefundFailed,
        EventTypes.RefundUpdated
    ];

    private readonly IPaymentModelsResolver _paymentModelsResolver;
    private readonly IPaymentsRepository _paymentRepository;
    private readonly IRefundModelsResolver _refundModelsResolver;
    private readonly IOrdersRepository _ordersRepository;
    private readonly IOrderModelsResolver _orderModelsResolver;
    private readonly IOrderService _orderService;
    private readonly IRefundsRepository _refundsRepository;

    public PaymentProviderType Type => PaymentProviderType.Stripe;

    public StripePaymentProviderService(
        IPaymentModelsResolver paymentModelResolver,
        IPaymentsRepository paymentsRepository,
        IRefundModelsResolver refundModelsResolver,
        IOrdersRepository ordersRepository,
        IOrderModelsResolver orderModelsResolver,
        IOrderService orderService,
        IRefundsRepository refundsRepository
    )
    {
        _paymentModelsResolver = paymentModelResolver;
        _paymentRepository = paymentsRepository;
        _refundModelsResolver = refundModelsResolver;
        _ordersRepository = ordersRepository;
        _orderModelsResolver = orderModelsResolver;
        _orderService = orderService;
        _refundsRepository = refundsRepository;
    }

    public async Task<bool> Validate(string externalId, string apiSecret)
    {
        var service = new AccountService();

        try
        {
            await service.GetAsync(externalId, null, GetRequestOptions(externalId, apiSecret));
        }
        catch (StripeException)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> ValidateTerminal(PaymentProviderDao paymentProviderDao, string externalId)
    {
        var service = new ReaderService();

        try
        {
            await service.GetAsync(externalId, null, GetRequestOptions(paymentProviderDao));
        }
        catch (StripeException)
        {
            return false;
        }

        return true;
    }

    public async Task<PaymentDao> HandlePayment(
        PaymentDao paymentDao,
        PaymentProviderDao paymentProviderDao,
        PaymentTerminalDao paymentTerminalDao)
    {
        var paymentIntent = await CreatePaymentIntent(paymentDao, paymentProviderDao);
        var reader = await ProcessPaymentIntent(paymentIntent, paymentTerminalDao, paymentProviderDao);

        return UpdatePaymentDao(paymentDao, reader);
    }

    public async Task<RefundDao> HandleRefund(RefundDao refundDao, PaymentDao paymentDao, PaymentProviderDao paymentProviderDao)
    {
        var refund = await CreateRefund(refundDao, paymentDao, paymentProviderDao);

        return UpdateRefundDao(refundDao, refund);
    }

    public async Task HandleWebhook(HttpRequest request, PaymentProviderDao paymentProviderDao)
    {
        var json = await new StreamReader(request.Body).ReadToEndAsync();
        var stripeEvent = EventUtility.ConstructEvent(json, request.Headers["Stripe-Signature"], paymentProviderDao.WebhookSecret);

        if (_paymentWebhookEventTypes.Any(stripeEvent.Type.Contains))
        {
            await HandlePaymentWebhook(paymentProviderDao, (PaymentIntent)stripeEvent.Data.Object);

            return;
        }

        if (_refundWebhookEventTypes.Any(stripeEvent.Type.Contains))
        {
            await HandleRefundWebhook(paymentProviderDao, (Refund)stripeEvent.Data.Object);
        }
    }

    private async Task HandlePaymentWebhook(PaymentProviderDao paymentProviderDao, PaymentIntent paymentIntent)
    {
        var paymentDao = await _paymentRepository.GetAsyncByExternalId(paymentIntent.Id);

        if (paymentDao.PaymentTerminal!.PaymentProviderId != paymentProviderDao.Id)
        {
            throw new LoomsBadRequestException("Mismatched payment provider");
        }

        paymentDao = UpdatePaymentDao(paymentDao, paymentIntent);

        await _paymentRepository.UpdateAsync(paymentDao);

        if (paymentDao.Status is not PaymentStatus.Succeeded)
        {
            return;
        }

        var orderDao = await _ordersRepository.GetAsync(paymentDao.OrderId);
        var payableAmount = _orderService.CalculatePayableAmount(orderDao);

        if (payableAmount is not 0)
        {
            return;
        }

        orderDao = _orderModelsResolver.GetDaoFromDaoAndStatus(orderDao, OrderStatus.Paid);
        await _ordersRepository.UpdateAsync(orderDao);
    }

    private async Task HandleRefundWebhook(PaymentProviderDao paymentProviderDao, Refund refund)
    {
        var refundDao = await _refundsRepository.GetAsyncByExternalId(refund.Id);

        if (refundDao.Payment.PaymentTerminal!.PaymentProviderId != paymentProviderDao.Id)
        {
            throw new LoomsBadRequestException("Mismatched payment provider");
        }

        refundDao = UpdateRefundDao(refundDao, refund);

        await _refundsRepository.UpdateAsync(refundDao);
    }

    private async Task<PaymentIntent> CreatePaymentIntent(PaymentDao paymentDao, PaymentProviderDao paymentProviderDao)
    {
        var options = new PaymentIntentCreateOptions
        {
            Currency = "eur", PaymentMethodTypes = ["card_present"], CaptureMethod = "automatic", Amount = (long)(paymentDao.Amount * 100)
        };

        var service = new PaymentIntentService();

        return await service.CreateAsync(options, GetRequestOptions(paymentProviderDao));
    }

    private async Task<Reader> ProcessPaymentIntent(
        PaymentIntent paymentIntent,
        PaymentTerminalDao paymentTerminalDao,
        PaymentProviderDao paymentProviderDao
    )
    {
        var options = new ReaderProcessPaymentIntentOptions { PaymentIntent = paymentIntent.Id };
        var service = new ReaderService();

        return await service.ProcessPaymentIntentAsync(paymentTerminalDao.ExternalId, options, GetRequestOptions(paymentProviderDao));
    }

    private async Task<Refund> CreateRefund(RefundDao refundDao, PaymentDao paymentDao, PaymentProviderDao paymentProviderDao)
    {
        var options = new RefundCreateOptions { PaymentIntent = paymentDao.ExternalId, Amount = (long)(refundDao.Amount * 100) };

        var service = new Stripe.RefundService();

        return await service.CreateAsync(options, GetRequestOptions(paymentProviderDao));
    }

    private PaymentDao UpdatePaymentDao(PaymentDao paymentDao, Reader reader)
    {
        var status = reader.Action.Status switch
        {
            "in_progress" => PaymentStatus.Pending,
            "succeeded" => PaymentStatus.Succeeded,
            "failed" => PaymentStatus.Failed,
            _ => throw new Exception($"Unknown reader action status: {reader.Action}")
        };

        paymentDao = _paymentModelsResolver.GetDaoFromDaoAndStatus(paymentDao, status);

        return _paymentModelsResolver.GetDaoFromDaoAndExternalId(paymentDao, reader.Action.ProcessPaymentIntent.PaymentIntentId);
    }

    private PaymentDao UpdatePaymentDao(PaymentDao paymentDao, PaymentIntent paymentIntent)
    {
        var status = paymentIntent.Status switch
        {
            "requires_confirmation" or "requires_action" or "processing" => PaymentStatus.Pending,
            "requires_payment_method" or "canceled" => PaymentStatus.Failed,
            "succeeded" => PaymentStatus.Succeeded,
            _ => throw new Exception($"Unknown payment intent status: {paymentIntent.Status}")
        };

        paymentDao = _paymentModelsResolver.GetDaoFromDaoAndStatus(paymentDao, status);

        return _paymentModelsResolver.GetDaoFromDaoAndExternalId(paymentDao, paymentIntent.Id);
    }

    private RefundDao UpdateRefundDao(RefundDao refundDao, Refund refund)
    {
        var status = refund.Status switch
        {
            "pending" or "requires_action" => RefundStatus.Pending,
            "failed" or "canceled" => RefundStatus.Rejected,
            "succeeded" => RefundStatus.Completed,
            _ => throw new Exception($"Unknown refund status: {refund.Status}")
        };

        refundDao = _refundModelsResolver.GetDaoFromDaoAndStatus(refundDao, status);

        return _refundModelsResolver.GetDaoFromDaoAndExternalId(refundDao, refund.Id);
    }

    private RequestOptions GetRequestOptions(string externalId, string apiSecret)
    {
        return new RequestOptions { StripeAccount = externalId, ApiKey = apiSecret };
    }

    private RequestOptions GetRequestOptions(PaymentProviderDao paymentProviderDao)
    {
        return GetRequestOptions(paymentProviderDao.ExternalId, paymentProviderDao.ApiSecret);
    }
}
