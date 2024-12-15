using FluentValidation;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Discount.Commands.CreateDiscount;

public class CreateOrderItemRequestValidator : AbstractValidator<CreateOrderItemRequest>
{
    public CreateOrderItemRequestValidator(
        IDiscountsRepository discountsRepository,
        IProductVariationRepository variationRepository,
        IProductsRepository productsRepository)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x)
            .Must(x => x.ServiceId is not null || x.ProductId is not null)
            .WithMessage("ServiceId or ProductId is required.");

// TODO: add rules for service ids with custom async validation like from GetOrderItemsQueryValidator

        RuleFor(x => x.ProductId!)
            .MustBeValidGuid()
            .CustomAsync(async (productId, context, cancellationToken) => 
            {
                var product = await productsRepository.GetAsync(Guid.Parse(productId));

                if(product!.Quantity < context.InstanceToValidate.Quantity)
                {
                    context.AddFailure("Product quantity is too low.");
                }

            })
            .When(x => x.ServiceId is null && x.ProductId is not null);

        RuleFor(x => x.ProductVariationId!)
            .MustBeValidGuid()
            .CustomAsync(async (variationId, context, cancellationToken) => 
            {
                var variation = await variationRepository.GetAsync(Guid.Parse(variationId));

                if(variation!.Quantity < context.InstanceToValidate.Quantity)
                {
                    context.AddFailure("Variation quantity is too low.");
                }
            })
            .When(x => x.ServiceId is null && x.ProductId is not null && x.ProductVariationId is not null);

        RuleFor(x => x.DiscountId!)
            .MustBeValidGuid()
            .CustomAsync(async (discountId, context, cancellationToken) =>
            {
                var discount = await discountsRepository.GetAsync(Guid.Parse(discountId));
                
                if (discount.Target == DiscountTarget.Order)
                {
                    context.AddFailure("Discount cannot be applied to order item.");
                }
                else if(context.InstanceToValidate.ProductId is not null && discount.ProductId != Guid.Parse(context.InstanceToValidate.ProductId))
                {
                    context.AddFailure("Discount cannot be applied to product item.");
                }
                else if(context.InstanceToValidate.ServiceId is not null && discount.ProductId != Guid.Parse(context.InstanceToValidate.ServiceId))
                {
                    context.AddFailure("Discount cannot be applied to service item.");
                }
            })
            .When(x => x.DiscountId is not null);

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .GreaterThan(0);
    }
}