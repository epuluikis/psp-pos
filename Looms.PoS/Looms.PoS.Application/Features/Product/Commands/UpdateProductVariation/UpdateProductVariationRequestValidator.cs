using FluentValidation;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Models.Requests.Product;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Product.Commands.UpdateProductVariation;

public class UpdateProductVariationRequestValidator : AbstractValidator<UpdateProductVariationRequest>
{
    public UpdateProductVariationRequestValidator(IProductsRepository productsRepository)
    {
        RuleFor(x => x.ProductId)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (id, context, _)
                => await productsRepository.GetAsyncByIdAndBusinessId(Guid.Parse(id!),
                            Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader])));

        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Price)
            .PrecisionScale(10, 2, false)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.QuantityInStock)
            .GreaterThanOrEqualTo(0);
    }
}
