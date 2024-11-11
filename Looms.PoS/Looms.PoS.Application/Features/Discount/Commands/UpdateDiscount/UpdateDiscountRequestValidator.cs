using FluentValidation;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Features.Discount.Commands.UpdateDiscount;
public class UpdateDiscountRequestValidator : AbstractValidator<UpdateDiscountRequest>
{
    public UpdateDiscountRequestValidator()
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
            .GreaterThan(0)
            .WithMessage("Value must be greater than 0");
        RuleFor(x => x.StartDate)
            .NotEmpty()
            .Must(x => x != default(DateTime) && x >= DateTime.Now)
            .WithMessage("Start date must be in the future");
        RuleFor(x => x.EndDate)
            .NotEmpty()
            .Must(x => x != default(DateTime) && x > DateTime.Now)
            .WithMessage("End date must be in the future");
        RuleFor(x => x)
            .Must(x => x.EndDate > x.StartDate)
            .WithMessage("End date must be after the start date");
    }
}