using FluentValidation;
using Looms.PoS.Application.Models.Requests.Product;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Product.Commands.CreateProduct;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator(ITaxesRepository taxesRepository)
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.TaxId!)
            .MustBeValidGuid()
            .CustomAsync(async (taxId, context, cancellationToken) =>
                await taxesRepository.GetAsync(Guid.Parse(taxId)))
            .When(x => x.TaxId is not null);

        RuleFor(x => x.Price)
            .PrecisionScale(10, 2, false)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.QuantityInStock)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Description)
            .NotEmpty();
    }
}
