using FluentValidation;
using Looms.PoS.Application.Constants;
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

        RuleFor(x => x.DiscountId)
            .MustBeValidGuid()
            .CustomAsync(async (discountId, context, _) =>
            {
                var discountDao = await discountsRepository.GetAsyncByIdAndBusinessId(
                    Guid.Parse(discountId!),
                    Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader])
                );

                if (discountDao.Target != DiscountTarget.Order)
                {
                    context.AddFailure("Discount cannot be applied to order.");
                }
            }).When(x => x.DiscountId is not null);
    }
}
