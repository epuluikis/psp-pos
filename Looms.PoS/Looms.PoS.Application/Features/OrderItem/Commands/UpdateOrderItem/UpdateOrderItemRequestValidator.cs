using FluentValidation;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Models.Requests.OrderItem;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.OrderItem.Commands.UpdateOrderItem;

public class UpdateOrderItemRequestValidator : AbstractValidator<UpdateOrderItemRequest>
{
    public UpdateOrderItemRequestValidator(
        IDiscountsRepository discountsRepository,
        IProductVariationRepository variationRepository,
        IProductsRepository productsRepository,
        IServicesRepository servicesRepository,
        IOrderItemsRepository orderItemsRepository
    )
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x)
            .Must(x => x.ServiceId is not null && x.ProductId is null || x.ProductId is not null && x.ServiceId is null)
            .WithMessage("ReservationId or ProductId is required.");

        RuleFor(x => x.ServiceId)
            .MustBeValidGuid()
            .CustomAsync(async (serviceId, context, _)
                => await servicesRepository.GetAsyncByIdAndBusinessId(
                    Guid.Parse(serviceId!),
                    Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader])
                )
            ).When(x => x.ServiceId is not null);

        RuleFor(x => x.ProductId)
            .MustBeValidGuid()
            .CustomAsync(async (productId, context, _) =>
            {
                var orderItemDao = await orderItemsRepository.GetAsync(Guid.Parse((string)context.RootContextData["Id"]));
                var productDao = await productsRepository.GetAsyncByIdAndBusinessId(
                    Guid.Parse(productId!),
                    Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader])
                );

                if (productDao.Quantity + orderItemDao.Quantity < context.InstanceToValidate.Quantity)
                {
                    context.AddFailure("Product quantity is too low.");
                }
            })
            .When(x => x.ProductId is not null);

        RuleFor(x => x.ProductVariationId)
            .Null()
            .When(x => x.ProductId is null)
            .WithMessage("ProductVariation is only allowed with Product.");

        RuleFor(x => x.ProductVariationId)
            .MustBeValidGuid()
            .CustomAsync(async (variationId, context, _) =>
            {
                var orderItemDao = await orderItemsRepository.GetAsync(Guid.Parse((string)context.RootContextData["Id"]));
                var variation = await variationRepository.GetAsyncByIdAndProductId(
                    Guid.Parse(variationId!),
                    Guid.Parse(context.InstanceToValidate.ProductId!)
                );

                if (variation.Quantity + orderItemDao.Quantity < context.InstanceToValidate.Quantity)
                {
                    context.AddFailure("Variation quantity is too low.");
                }
            })
            .When(x => x.ProductVariationId is not null);

        RuleFor(x => x.DiscountId)
            .MustBeValidGuid()
            .CustomAsync(async (discountId, context, _) =>
            {
                var discount = await discountsRepository.GetAsyncByIdAndBusinessId(
                    Guid.Parse(discountId!),
                    Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader])
                );

                if (discount.Target == DiscountTarget.Order)
                {
                    context.AddFailure("Discount cannot be applied to order item.");

                    return;
                }

                if (context.InstanceToValidate.ProductId is not null
                 && Guid.TryParse(context.InstanceToValidate.ProductId, out var productId)
                 && discount.ProductId != productId)
                {
                    context.AddFailure("Discount cannot be applied to product item.");
                }
            })
            .When(x => x.DiscountId is not null);

        RuleFor(x => x.Quantity)
            .NotEmpty()
            .GreaterThan(0);
    }
}
