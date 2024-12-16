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
        IProductsRepository productsRepository,
        IReservationsRepository reservationsRepository)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x)
            .Must(x => x.ReservationId is not null || x.ProductId is not null)
            .WithMessage("ReservationId or ProductId is required.");

        RuleFor(x => x.ReservationId!)
            .MustBeValidGuid()
            .CustomAsync(async (reservationId, context, cancellationToken) => 
            {
                var reservation = await reservationsRepository.GetAsync(Guid.Parse(reservationId));

                if(reservation!.Status != ReservationStatus.Booked)
                {
                    context.AddFailure("Reservation is not available.");
                }
            })
            .When(x => x.ReservationId is not null);

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
            .When(x => x.ReservationId is null && x.ProductId is not null);

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
            .When(x => x.ReservationId is null && x.ProductId is not null && x.ProductVariationId is not null);

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
                else if(context.InstanceToValidate.ReservationId is not null && discount.ProductId != Guid.Parse(context.InstanceToValidate.ReservationId))
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