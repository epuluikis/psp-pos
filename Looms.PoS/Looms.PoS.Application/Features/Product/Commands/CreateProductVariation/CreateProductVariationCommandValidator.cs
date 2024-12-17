using FluentValidation;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests.Product;

namespace Looms.PoS.Application.Features.Product.Commands.CreateProductVariation;

public class CreateProductVariationCommandValidator : AbstractValidator<CreateProductVariationCommand>
{
    public CreateProductVariationCommandValidator(IHttpContentResolver httpContentResolver, IEnumerable<IValidator<CreateProductVariationRequest>> validators)
    {
        RuleFor(x => x.Request)
            .CustomAsync(async (request, context, cancellationToken) =>
            {
                var body = await httpContentResolver.GetPayloadAsync<CreateProductVariationRequest>(request);

                var validationResults = validators.Select(x => x.ValidateAsync(body));
                await Task.WhenAll(validationResults);

                foreach (var validationError in validationResults.SelectMany(x => x.Result.Errors))
                {
                    context.AddFailure(validationError);
                }
            });
    }
}
