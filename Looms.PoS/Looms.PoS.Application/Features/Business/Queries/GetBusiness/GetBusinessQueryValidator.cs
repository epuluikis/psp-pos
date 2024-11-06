using FluentValidation;
using Looms.PoS.Application.Utilities.Validators;

namespace Looms.PoS.Application.Features.Business.Queries.GetBusiness;

public class GetBusinessQueryValidator : AbstractValidator<GetBusinessQuery>
{
    public GetBusinessQueryValidator()
    {
        RuleFor(x => x.Id).MustBeValidGuid();
    }
}
