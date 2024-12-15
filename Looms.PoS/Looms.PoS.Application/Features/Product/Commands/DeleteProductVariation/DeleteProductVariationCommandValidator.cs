using FluentValidation;
using Looms.PoS.Domain.Interfaces;
using Looms.PoS.Application.Utilities.Validators;

namespace Looms.PoS.Application.Features.Product.Commands.DeleteProductVariation;

public class DeleteProductVariationCommandValidator : AbstractValidator<DeleteProductVariationCommand>
{
    public DeleteProductVariationCommandValidator(IProductVariationRepository productVariationRepository)
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (id, _, cancellationToken) => await productVariationRepository.GetAsync(Guid.Parse(id)));
    }
}
