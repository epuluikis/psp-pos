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
        IDiscountsRepository discountsRepository)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x)
            .Must(x => x.ServiceId != null || x.ProductId != null)
            .WithMessage("ServiceId or ProductId is required.");

        RuleFor(x => x.OrderId)
        .Cascade(CascadeMode.Stop)
        .MustBeValidGuid()
        .CustomAsync(async (orderId, _, cancellationToken) => 
        await ordersRepository.GetAsync(Guid.Parse(orderId)));

// TODO: add rules for service, product and variation ids with custom async validation like from GetOrderItemsQueryValidator
        
// TODO: change this when varuation is added

        // RuleFor(x => x.VariationId!)
        //     .MustBeValidGuid()
        //     .CustomAsync(async (variationId, _, cancellationToken) => 
        // await variationRepository.GetAsync(Guid.Parse(variationId)))
        //     .When(x => x.ServiceId == null && x.ProductId != null);

        RuleFor(x => x.DiscountId!)
            .MustBeValidGuid()
            .CustomAsync(async (discountId, context, cancellationToken) =>
            {
                var discount = await discountsRepository.GetAsync(Guid.Parse(discountId));
                if (discount == null)
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