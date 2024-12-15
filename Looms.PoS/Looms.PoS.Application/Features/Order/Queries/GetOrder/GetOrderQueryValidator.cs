using FluentValidation;
using Looms.PoS.Application.Utilities.Validators;

namespace Looms.PoS.Application.Features.Order.Queries.GetOrder;

public class GetOrderQueryValidator : AbstractValidator<GetOrderQuery>
{
    public GetOrderQueryValidator()
    {
        RuleFor(x => x.Id)
            .MustBeValidGuid();
    }
}
