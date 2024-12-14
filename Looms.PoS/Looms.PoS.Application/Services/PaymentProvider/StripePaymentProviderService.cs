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
    private readonly string[] _supportedWebhookEventTypes =
    [
        EventTypes.PaymentIntentCanceled,
        EventTypes.PaymentIntentPaymentFailed,
        EventTypes.PaymentIntentProcessing,
        EventTypes.PaymentIntentRequiresAction,
        EventTypes.PaymentIntentSucceeded
    ];

    private readonly IPaymentModelsResolver _paymentModelsResolver;
    private readonly IPaymentsRepository _paymentRepository;

    public PaymentProviderType Type => PaymentProviderType.Stripe;

    public StripePaymentProviderService(
        IPaymentModelsResolver paymentModelResolver,
        IPaymentsRepository paymentsRepository
    )
    {
        _paymentModelsResolver = paymentModelResolver;
        _paymentRepository = paymentsRepository;
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

    public async Task HandleWebhook(HttpRequest request, PaymentProviderDao paymentProviderDao)
    {
        var json = await new StreamReader(request.Body).ReadToEndAsync();
        var stripeEvent = EventUtility.ConstructEvent(json, request.Headers["Stripe-Signature"], paymentProviderDao.WebhookSecret);

        if (!_supportedWebhookEventTypes.Any(stripeEvent.Type.Contains))
        {
            return;
        }

        var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
        var paymentDao = await _paymentRepository.GetAsyncByExternalId(paymentIntent.Id);

        if (paymentDao.PaymentTerminal!.PaymentProviderId != paymentProviderDao.Id)
        {
            throw new LoomsBadRequestException("Mismatched payment provider");
        }

        paymentDao = UpdatePaymentDao(paymentDao, paymentIntent);

        await _paymentRepository.UpdateAsync(paymentDao);
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

    private RequestOptions GetRequestOptions(string externalId, string apiSecret)
    {
        return new RequestOptions { StripeAccount = externalId, ApiKey = apiSecret };
    }

    private RequestOptions GetRequestOptions(PaymentProviderDao paymentProviderDao)
    {
        return GetRequestOptions(paymentProviderDao.ExternalId, paymentProviderDao.ApiSecret);
    }
}
