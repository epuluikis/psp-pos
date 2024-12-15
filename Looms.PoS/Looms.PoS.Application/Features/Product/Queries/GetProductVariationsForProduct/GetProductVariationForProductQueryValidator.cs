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
                await productsRepository.GetAsync(Guid.Parse(id)));
    }
}
