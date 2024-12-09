using FluentValidation;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Order.Queries.GetOrders;

public class GetOrdersQueryValidator : AbstractValidator<GetOrdersQuery>
{
    public GetOrdersQueryValidator(IOrdersRepository ordersRepository)
    {
        RuleFor(x => x.Status)
            .Must(x => Enum.TryParse<OrderStatus>(x, out _))
            .When(x => x.Status != null);

        RuleFor(x => x.UserId)
            .Must(x => Guid.TryParse(x, out _))
            .When(x => x.UserId != null);
    }
}
