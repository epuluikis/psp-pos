using FluentValidation;
using Looms.PoS.Application.Utilities.Validators;

namespace Looms.PoS.Application.Features.Tax.Queries.GetTax;

public class GetTaxQueryValidator : AbstractValidator<GetTaxQuery>
{
    public GetTaxQueryValidator()
    {
        RuleFor(x => x.Id).MustBeValidGuid();
    }
}
