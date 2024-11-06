using FluentValidation;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests;
using System.Threading;

namespace Looms.PoS.Application.Features.Discount.Commands;
public class CreateDiscountsCommandValidator : AbstractValidator<CreateDiscountsCommand>
{
    public CreateDiscountsCommandValidator(IHttpContentResolver httpContentResolver, IEnumerable<IValidator<CreateDiscountRequest>> validators)
    {
        RuleFor(x => x.Request)
            .CustomAsync(async (request, context, cancellationToken) =>
            {
                var body = await httpContentResolver.GetPayloadAsync<CreateDiscountRequest>(request);

                var validationResults = validators.Select(x => x.ValidateAsync(body));
                await Task.WhenAll(validationResults);

                foreach (var validationError in validationResults.SelectMany(x => x.Result.Errors))
                {
                    context.AddFailure(validationError);
                }
            });
    }
}
