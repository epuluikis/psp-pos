using FluentValidation;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Discount.Commands.DeleteDiscount;

public class DeleteDiscountCommandValidator : AbstractValidator<DeleteDiscountCommand>
{
    public DeleteDiscountCommandValidator(
        IDiscountsRepository discountsRepository)
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (id, _, cancellationToken) => await discountsRepository.GetAsync(Guid.Parse(id)));
    }
}