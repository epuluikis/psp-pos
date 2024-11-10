using FluentValidation;
using Looms.PoS.Application.Utilities.Validators;

namespace Looms.PoS.Application.Features.Payment.Queries.GetPayment;

public class GetPaymentQueryValidator : AbstractValidator<GetPaymentQuery>
{
    public GetPaymentQueryValidator()
    {
        RuleFor(x => x.Id).MustBeValidGuid();
    }
}
