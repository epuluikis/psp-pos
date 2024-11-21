using FluentValidation;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Utilities;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Features.Discount.Commands.UpdateDiscount;

// TODO : Add checking for dates, if they are the same as in the database, otherwise need to be in the future

public class UpdateDiscountRequestValidator : AbstractValidator<UpdateDiscountRequest>
{
    public UpdateDiscountRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.DiscountTarget)
            .NotEmpty()
            .Must(value => Enum.TryParse<DiscountTarget>(value, true, out _))
            .WithMessage("Invalid DiscountTarget value.");
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .When(x =>
                Enum.TryParse<DiscountTarget>(x.DiscountTarget, true, out var discountTarget) && 
                discountTarget == DiscountTarget.Product)
            .WithMessage("ProductId is required when discount is for product.");
        RuleFor(x => x.ProductId)
            .Empty()
            .When(x =>
                Enum.TryParse<DiscountTarget>(x.DiscountTarget, true, out var discountTarget) && 
                discountTarget == DiscountTarget.Order)
            .WithMessage("ProductId must be empty when discount is for orders.");
        RuleFor(x => x.DiscountType)
            .NotEmpty()
            .Must(value => Enum.TryParse<DiscountType>(value, true, out _))
            .WithMessage("Invalid DiscountType value.");
        RuleFor(x => x.Value)
            .NotEmpty()
            .GreaterThan(0)
            .WithMessage("Value must be greater than 0");
        RuleFor(x => x.StartDate)
            .NotEmpty()
            .Must(dateString =>
            {
                return DateTimeHelper.TryConvertToUtc(dateString);
            });
        RuleFor(x => x.EndDate)
            .NotEmpty()
            .Must(dateString =>
            {
                return DateTimeHelper.TryConvertToUtc(dateString);
            });
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