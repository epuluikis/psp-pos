using FluentValidation;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests.ProductVariation;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Product.Commands.UpdateProductVariation;

public class UpdateProductVariationCommandValidator : AbstractValidator<UpdateProductVariationCommand>
{
    public UpdateProductVariationCommandValidator(
        IHttpContentResolver httpContentResolver,
        IEnumerable<IValidator<UpdateProductVariationRequest>> validators,
        IProductVariationRepository productVariationRepository)
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (id, _, cancellationToken) => await productVariationRepository.GetAsync(Guid.Parse(id)));

        RuleFor(x => x.Request)
            .CustomAsync(async (request, context, cancellationToken) =>
            {
                var body = await httpContentResolver.GetPayloadAsync<UpdateProductVariationCommand>(request);

                var validationResults = validators.Select(x => x.ValidateAsync((IValidationContext)body));
                await Task.WhenAll(validationResults);

                foreach (var validationError in validationResults.SelectMany(x => x.Result.Errors))
                {
                    context.AddFailure(validationError);
                }
            });
    }
}
