using FluentValidation;
using Looms.PoS.Domain.Interfaces;
using Looms.PoS.Application.Utilities.Validators;

namespace Looms.PoS.Application.Features.Product.Commands.DeleteProduct;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator(IProductsRepository productsRepository)
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (id, _, cancellationToken) => await productsRepository.GetAsync(Guid.Parse(id)));
    }
}
