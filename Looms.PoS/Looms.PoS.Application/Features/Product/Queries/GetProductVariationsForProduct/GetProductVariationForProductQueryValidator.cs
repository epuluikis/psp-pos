using FluentValidation;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Product.Queries.GetProductVariationForProduct;

public class GetProductVariationForProductQueryValidator : AbstractValidator<GetProductVariationForProductQuery>
{
    public GetProductVariationForProductQueryValidator(IProductsRepository productsRepository)
    {
        RuleFor(x => x.Id)
            .MustBeValidGuid()
            .CustomAsync(async (id, context, cancellationToken) => 
            {
                var product = await productsRepository.GetAsync(Guid.Parse(id));
                if (product is null)
                {
                    context.AddFailure("Product not found");
                }
            });
    }
}
