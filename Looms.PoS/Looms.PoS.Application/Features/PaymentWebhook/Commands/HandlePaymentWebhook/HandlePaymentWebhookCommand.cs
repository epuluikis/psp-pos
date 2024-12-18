using Looms.PoS.Application.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Looms.PoS.Application.Features.PaymentWebhook.Commands.HandlePaymentWebhook;

public record HandlePaymentWebhookCommand : WebhookRequest, IRequest<IActionResult>
{
    public string PaymentProviderId { get; init; }

    public HandlePaymentWebhookCommand(HttpRequest request, string paymentProviderId) : base(request)
    {
        PaymentProviderId = paymentProviderId;
    }
}
