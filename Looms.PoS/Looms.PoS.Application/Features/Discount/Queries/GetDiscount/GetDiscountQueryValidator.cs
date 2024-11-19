using FluentValidation;
using Looms.PoS.Application.Utilities.Validators;

namespace Looms.PoS.Application.Features.Discount.Queries.GetDiscount;

public class GetDiscountQueryValidator : AbstractValidator<GetDiscountQuery>
{
    public GetDiscountQueryValidator()
    {
        RuleFor(x => x.Id).MustBeValidGuid();
    }
}
