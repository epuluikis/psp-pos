using FluentValidation;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Order.Commands.CreateOrder;

public class UpdateOrderRequestValidator : AbstractValidator<UpdateOrderRequest>
{
    public UpdateOrderRequestValidator(IDiscountsRepository discountsRepository)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.DiscountId!)
            .MustBeValidGuid()
            .CustomAsync(async (discountId, _, cancellationToken) => {
                Console.WriteLine(discountId);
                Console.WriteLine(Guid.Parse(discountId));
                if (discountId != null)
                {
                    var discount = await discountsRepository.GetAsync(Guid.Parse(discountId)) ?? throw new LoomsNotFoundException("Discount n found");
                }
            })
        .When(x => x.DiscountId != null);

        RuleFor(x => x.Status)
            .Must(value => Enum.TryParse<OrderStatus>(value, true, out _))
            .When(x => x.Status != null);
    }
}
