using FluentValidation;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests.Product;

namespace Looms.PoS.Application.Features.Product.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator(IHttpContentResolver httpContentResolver, IEnumerable<IValidator<CreateProductRequest>> validators)
    {
        RuleFor(x => x.Request)
            .CustomAsync(async (request, context, cancellationToken) =>
            {
                var body = await httpContentResolver.GetPayloadAsync<CreateProductRequest>(request);

                var validationResults = validators.Select(x => x.ValidateAsync(body));
                await Task.WhenAll(validationResults);

                foreach (var validationError in validationResults.SelectMany(x => x.Result.Errors))
                {
                    context.AddFailure(validationError);
                }
            });
    }
}
