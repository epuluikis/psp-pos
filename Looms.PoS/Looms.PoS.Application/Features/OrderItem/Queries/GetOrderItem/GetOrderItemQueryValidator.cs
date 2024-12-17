using FluentValidation;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.OrderItem.Queries.GetOrderItem;

public class GetOrderItemQueryValidator : AbstractValidator<GetOrderItemQuery>
{
    public GetOrderItemQueryValidator()
    {
        RuleFor(x => x.OrderId)
            .MustBeValidGuid();

        RuleFor(x => x.Id)
            .MustBeValidGuid();
    }
}
