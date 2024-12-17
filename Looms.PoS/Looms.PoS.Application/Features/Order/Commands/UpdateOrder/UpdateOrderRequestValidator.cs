using FluentValidation;
using Looms.PoS.Application.Models.Requests.Order;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Order.Commands.UpdateOrder;

public class UpdateOrderRequestValidator : AbstractValidator<UpdateOrderRequest>
{
    public UpdateOrderRequestValidator(IDiscountsRepository discountsRepository)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.DiscountId!)
            .MustBeValidGuid()
            .CustomAsync(async (discountId, _, cancellationToken) =>
                await discountsRepository.GetAsync(Guid.Parse(discountId)))
            .When(x => x.DiscountId is not null);

        RuleFor(x => x.Status)
            .Must(value => Enum.TryParse<OrderStatus>(value, true, out _))
            .When(x => x.Status is not null);
    }
}
