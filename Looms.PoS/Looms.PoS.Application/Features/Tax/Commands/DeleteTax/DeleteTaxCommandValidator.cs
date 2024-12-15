using FluentValidation;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Tax.Commands.DeleteTax;

public class DeleteTaxCommandValidator : AbstractValidator<DeleteTaxCommand>
{
    public DeleteTaxCommandValidator(
        ITaxesRepository taxesRepository)
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (id, _, cancellationToken) => await taxesRepository.GetAsync(Guid.Parse(id)));
    }
}
