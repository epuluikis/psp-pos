using FluentValidation;
using Looms.PoS.Application.Models.Requests.Product;

namespace Looms.PoS.Application.Features.Product.Commands.UpdateProduct;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Price)
            .PrecisionScale(10, 2, false)
            .GreaterThanOrEqualTo(0);
        
        RuleFor(x => x.QuantityInStock)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Description)
            .NotEmpty();

        //TODO: figure out how to do IEnumerable rules
        // RuleFor(x => x.VariationRequest.Name)
        //     .NotEmpty();
    }
}
