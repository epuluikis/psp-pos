using FluentValidation;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Application.Models.Requests.Tax;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Features.Tax.Commands.UpdateTax;

public class UpdateTaxRequestValidator : AbstractValidator<UpdateTaxRequest>
{
    public UpdateTaxRequestValidator()
    {
        RuleFor(x => x.Percentage)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .InclusiveBetween(0, 100)
            .WithMessage("Percentage must be between 0 and 100.");

        RuleFor(x => x.TaxCategory)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Must(value => Enum.TryParse(typeof(TaxCategory), value, true, out _))
            .WithMessage("Invalid TaxCategory value.");

        RuleFor(x => x.StartDate)
            .Cascade(CascadeMode.Stop)
            .MustBeValidDateTime();
    }
}
