using FluentValidation;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Utilities;
using Looms.PoS.Domain.Enums;
using System.Globalization;

namespace Looms.PoS.Application.Features.Discount.Commands.CreateDiscount;

public class CreateDiscountsRequestValidator : AbstractValidator<CreateDiscountRequest>
{
    public CreateDiscountsRequestValidator()
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
            .GreaterThan(0);
        RuleFor(x => x.StartDate)
            .NotEmpty()
            .Must(dateString =>
            {
                var parsedDate = DateTimeHelper.ConvertToUtc(dateString);
                return parsedDate >= DateTime.UtcNow;
            })
            .WithMessage("Start date must be a valid future date.");

        RuleFor(x => x.EndDate)
            .NotEmpty()
            .Must(dateString =>
            {
                var parsedDate = DateTimeHelper.ConvertToUtc(dateString);
                return parsedDate >= DateTime.UtcNow;
            })
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
