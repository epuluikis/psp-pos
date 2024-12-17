using FluentValidation;
using Looms.PoS.Application.Utilities.Validators;

namespace Looms.PoS.Application.Features.Refund.Queries.GetRefund;

public class GetRefundQueryValidator : AbstractValidator<GetRefundQuery>
{
    public GetRefundQueryValidator()
    {
        RuleFor(x => x.Id).MustBeValidGuid();
    }
}
