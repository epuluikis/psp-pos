using FluentValidation;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Discount.Commands.CreateDiscount;

public class CreateOrderItemRequestValidator : AbstractValidator<CreateOrderItemRequest>
{
    public CreateOrderItemRequestValidator(
        IOrdersRepository ordersRepository, 
        IDiscountsRepository discountsRepository,
        IProductVariationRepository variationRepository,
        IProductsRepository productsRepository)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x)
            .Must(x => x.ServiceId != null || x.ProductId != null)
            .WithMessage("ServiceId or ProductId is required.");

// TODO: add rules for service ids with custom async validation like from GetOrderItemsQueryValidator

        RuleFor(x => x.ProductId!)
            .MustBeValidGuid()
            .CustomAsync(async (productId, context, cancellationToken) => 
            {
                var product = await productsRepository.GetAsync(Guid.Parse(productId));
                if (product is null)
                {
                    context.AddFailure("Product not found.");
                }else{
                    if(product!.Quantity < context.InstanceToValidate.Quantity)
                    {
                        context.AddFailure("Product quantity is too low.");
                    }
                }

            })
            .When(x => x.ServiceId is null && x.ProductId != null);

        RuleFor(x => x.ProductVariationId!)
            .MustBeValidGuid()
            .CustomAsync(async (variationId, context, cancellationToken) => 
            {
                var variation = await variationRepository.GetAsync(Guid.Parse(variationId));
                if (variation is null)
                {
                    context.AddFailure("Variation not found.");

                    if(variation!.Quantity < context.InstanceToValidate.Quantity)
                    {
                        context.AddFailure("Variation quantity is too low.");
                    }
                }
            })
            .When(x => x.ServiceId is null && x.ProductId != null && x.ProductVariationId != null);

        RuleFor(x => x.DiscountId!)
            .MustBeValidGuid()
            .CustomAsync(async (discountId, context, cancellationToken) =>
            {
                var discount = await discountsRepository.GetAsync(Guid.Parse(discountId));
                if (discount is null)
                {
                    context.AddFailure("Discount not found.");
                }else if (discount.Target == DiscountTarget.Order)
                {
                    context.AddFailure("Discount cannot be applied to order item.");
                }else if(context.InstanceToValidate.ProductId != null && discount.ProductId != Guid.Parse(context.InstanceToValidate.ProductId))
                {
                    context.AddFailure("Discount cannot be applied to product item.");
                }else if(context.InstanceToValidate.ServiceId != null && discount.ProductId != Guid.Parse(context.InstanceToValidate.ServiceId))
                {
                    context.AddFailure("Discount cannot be applied to service item.");
                }
            })
            .When(x => x.DiscountId != null);

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .GreaterThan(0);
    }
}