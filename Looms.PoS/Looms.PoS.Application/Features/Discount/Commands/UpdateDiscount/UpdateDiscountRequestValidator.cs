using FluentValidation;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Models.Requests.Discount;
using Looms.PoS.Application.Utilities.Helpers;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Discount.Commands.UpdateDiscount;

public class UpdateDiscountRequestValidator : AbstractValidator<UpdateDiscountRequest>
{
    public UpdateDiscountRequestValidator(IProductsRepository productsRepository)
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.DiscountType)
            .NotNull()
            .IsInEnum();
        
        RuleFor(x => x.DiscountTarget)
            .NotEmpty()
            .IsInEnum();

        When(x => x.DiscountTarget == DiscountTarget.Product, () =>
        {
            RuleFor(x => x.ProductId)
                .MustBeValidGuid()
                .CustomAsync(async (productId, context, _)
                    => await productsRepository.GetAsyncByIdAndBusinessId(
                        Guid.Parse(productId!),
                        Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader])
                    )
                );
        });

        When(x => x.DiscountTarget != DiscountTarget.Product, () =>
        {
            RuleFor(x => x.ProductId)
                .Null()
                .WithMessage("ProductId must be empty when discount isn't for product.");
        });

        RuleFor(x => x.Value)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(x => x.StartDate)
            .NotEmpty()
            .Must(startDate => DateTimeHelper.ConvertToUtc(startDate) >= DateTime.UtcNow)
            .WithMessage("Start date must be a valid future date.");

        RuleFor(x => x.EndDate)
            .NotEmpty()
            .Must(endDate => DateTimeHelper.ConvertToUtc(endDate) >= DateTime.UtcNow)
            .WithMessage("End date must be a valid future date.");

        RuleFor(x => x)
            .Must(x =>
            {
                var startDate = DateTimeHelper.ConvertToUtc(x.StartDate);
                var endDate = DateTimeHelper.ConvertToUtc(x.EndDate);

                return endDate > startDate;
            })
            .WithMessage("End date must be after the start date.");
    }
}
