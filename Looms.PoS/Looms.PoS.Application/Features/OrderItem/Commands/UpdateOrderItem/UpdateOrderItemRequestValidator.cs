using FluentValidation;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Discount.Commands.CreateDiscount;

public class UpdateOrderItemRequestValidator : AbstractValidator<UpdateOrderItemRequest>
{
    public UpdateOrderItemRequestValidator(
        IDiscountsRepository discountsRepository,
        IProductVariationRepository variationsRepository)
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

                if (discount.Target == DiscountTarget.Order)
                {
                    context.AddFailure("Discount cannot be applied to order item.");
                }
            })
            .When(x => x.DiscountId is not null);

        RuleFor(x => x.VariationId!)
            .MustBeValidGuid()
            .CustomAsync(async (variationId, context, cancellationToken) =>
            {
                var variation = await variationsRepository.GetAsync(Guid.Parse(variationId));
                if (variation.Quantity < context.InstanceToValidate.Quantity)
                {
                    context.AddFailure("Product variation quantity is too low.");
                }
            })
            .When(x => x.VariationId is not null);
    }
}