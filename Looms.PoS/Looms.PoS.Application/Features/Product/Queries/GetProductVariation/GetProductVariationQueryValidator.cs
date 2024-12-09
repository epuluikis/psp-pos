using FluentValidation;
using Looms.PoS.Application.Utilities.Validators;

namespace Looms.PoS.Application.Features.Product.Queries.GetProductVariation;

public class GetProductVariationQueryValidator : AbstractValidator<GetProductVariationQuery>
{
    public GetProductVariationQueryValidator()
    {
        RuleFor(x => x.Id).MustBeValidGuid();
    }
}
