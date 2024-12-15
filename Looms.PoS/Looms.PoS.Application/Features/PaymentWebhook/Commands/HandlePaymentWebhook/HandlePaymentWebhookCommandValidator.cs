using FluentValidation;
using Looms.PoS.Application.Features.Product.Commands.DeleteProduct;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.PaymentWebhook.Commands.HandlePaymentWebhook;

public class HandlePaymentWebhookCommandValidator : AbstractValidator<HandlePaymentWebhookCommand>
{
    public HandlePaymentWebhookCommandValidator(
        IPaymentProvidersRepository paymentProvidersRepository
    )
    {
        RuleFor(x => x.PaymentProviderId)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (id, _, _) => await paymentProvidersRepository.GetAsync(Guid.Parse(id!)));
    }
}
