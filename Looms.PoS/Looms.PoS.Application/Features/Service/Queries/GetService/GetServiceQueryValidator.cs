using FluentValidation;
using Looms.PoS.Application.Utilities.Validators;

namespace Looms.PoS.Application.Features.Service.Queries.GetService;

public class GetServiceQueryValidator : AbstractValidator<GetServiceQuery>
{
    public GetServiceQueryValidator()
    {
        RuleFor(x => x.Id).MustBeValidGuid();
    }
}
