using FluentValidation;
using Looms.PoS.Application.Models.Requests.Payment;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Payment.Commands.CreatePayment;

public class CreatePaymentRequestValidator : AbstractValidator<CreatePaymentRequest>
{
    public CreatePaymentRequestValidator(
        IGiftCardsRepository giftCardsRepository,
        IPaymentTerminalsRepository paymentTerminalsRepository
    )
    {
        RuleFor(x => x.OrderId)
            .MustBeValidGuid();

        RuleFor(x => x.Amount)
            .Cascade(CascadeMode.Stop)
            .PrecisionScale(10, 2, false)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.PaymentMethod)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .IsInEnum();

        RuleFor(x => x.Tip)
            .Cascade(CascadeMode.Stop)
            .PrecisionScale(10, 2, false)
            .GreaterThanOrEqualTo(0);

        When(x => x.PaymentMethod == PaymentMethod.GiftCard, () =>
        {
            RuleFor(x => x.GiftCardCode)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .CustomAsync(async (code, _, _) =>
                {
                    // TODO: add legit business id
                    await giftCardsRepository.GetAsyncByBusinessIdAndCode(Guid.Empty, code!);
                });
        });

        When(x => x.PaymentMethod != PaymentMethod.GiftCard, () =>
        {
            RuleFor(x => x.GiftCardCode)
                .Null();
        });

        When(x => x.PaymentMethod != PaymentMethod.CreditCard, () =>
        {
            RuleFor(x => x.PaymentTerminalId)
                .Null();
        });

        When(x => x.PaymentMethod == PaymentMethod.CreditCard, () =>
        {
            RuleFor(x => x.PaymentTerminalId!)
                .MustBeValidGuid()
                .CustomAsync(async (id, _, _) =>
                {
                    await paymentTerminalsRepository.GetAsync(Guid.Parse(id!));
                });
        });
    }
}
