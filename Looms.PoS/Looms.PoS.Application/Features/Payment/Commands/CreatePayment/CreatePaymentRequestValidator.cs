using FluentValidation;
using Looms.PoS.Application.Models.Requests.Payment;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Features.Payment.Commands.CreatePayment;

public class CreatePaymentRequestValidator : AbstractValidator<CreatePaymentRequest>
{
    public CreatePaymentRequestValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEqual(Guid.Empty);

        When(x => x.PaymentMethod == PaymentMethod.GiftCard, () =>
        {
            RuleFor(x => x.GiftCardId)
                .NotNull()
                .NotEqual(Guid.Empty);
        });

        When(x => x.PaymentMethod != PaymentMethod.GiftCard, () =>
        {
            RuleFor(x => x.GiftCardId)
                .Null();
        });

        RuleFor(x => x.Amount)
            .PrecisionScale(10, 2, false)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.PaymentMethod)
            .NotNull()
            .IsInEnum();

        RuleFor(x => x.Tip)
            .PrecisionScale(10, 2, false)
            .GreaterThanOrEqualTo(0);
    }
}
