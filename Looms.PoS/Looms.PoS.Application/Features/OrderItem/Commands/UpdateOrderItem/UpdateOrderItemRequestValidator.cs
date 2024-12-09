using FluentValidation;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Discount.Commands.CreateDiscount;

public class UpdateOrderItemRequestValidator : AbstractValidator<UpdateOrderItemRequest>
{
    public UpdateOrderItemRequestValidator(
        IOrdersRepository ordersRepository, 
        IDiscountsRepository discountsRepository)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.DiscountId!)
            .MustBeValidGuid()
            .CustomAsync(async (discountId, context, cancellationToken) =>
            {
                var discount = await discountsRepository.GetAsync(Guid.Parse(discountId));
                if (discount == null)
                {
                    context.AddFailure("Discount not found.");
                }
                else if (discount.Target == DiscountTarget.Order)
                {
                    context.AddFailure("Discount cannot be applied to order item.");
                }
            })
            .When(x => x.DiscountId != null);

// TODO: uncomment this when variation is added

        // RuleFor(x => x.VariationId)
        //     .MustBeValidGuid()
        //     .CustomAsync(async (variationId, context, cancellationToken) =>
        //     {
        //         var variation = await variationsRepository.GetAsync(Guid.Parse(variationId));
        //         if (variation == null)
        //         {
        //             context.AddFailure("Variation not found.");
        //         }
        //     })
        //     .When(x => x.VariationId != null);


    }
}