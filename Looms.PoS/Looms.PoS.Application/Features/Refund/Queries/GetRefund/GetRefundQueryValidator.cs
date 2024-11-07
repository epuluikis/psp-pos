using FluentValidation;
using Looms.PoS.Application.Features.Refund.Queries.GetRefund;
using Looms.PoS.Application.Utilities.Validators;

namespace Looms.PoS.Application.Features.Refund.Queries.GetRefunds;

public class GetRefundsQueryValidator : AbstractValidator<GetRefundQuery>
{
    public GetRefundsQueryValidator()
    {
        RuleFor(x => x.Id).MustBeValidGuid();
    }
}
