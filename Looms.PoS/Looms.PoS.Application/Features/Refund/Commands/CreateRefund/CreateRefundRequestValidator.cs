using FluentValidation;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Application.Models.Requests;

namespace Looms.PoS.Application.Features.Refund.Commands.CreateRefund;

public class CreateRefundRequestValidator : AbstractValidator<CreateRefundRequest>
{
    public CreateRefundRequestValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThan(0);

        RuleFor(x => x.RefundReason)
            .NotEmpty();

        RuleFor(x => x.OrderId)
            .NotEmpty()
            .MustBeValidGuid();

        RuleFor(x => x.PaymentId)
            .NotEmpty()
            .MustBeValidGuid();
    }
}