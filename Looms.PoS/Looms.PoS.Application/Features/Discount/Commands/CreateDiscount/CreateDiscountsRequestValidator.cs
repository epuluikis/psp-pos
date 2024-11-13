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
            .IsInEnum();
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .When(x => x.DiscountTarget == DiscountTarget.Product);
        RuleFor(x => x.ProductId)
            .Empty()
            .When(x => x.DiscountTarget == DiscountTarget.Order);
        RuleFor(x => x.DiscountType)
            .IsInEnum();
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
