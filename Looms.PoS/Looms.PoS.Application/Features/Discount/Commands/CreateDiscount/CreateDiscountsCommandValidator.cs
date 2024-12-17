using FluentValidation;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests.Discount;

namespace Looms.PoS.Application.Features.Discount.Commands.CreateDiscount;

public class CreateDiscountsCommandValidator : AbstractValidator<CreateDiscountsCommand>
{
    public CreateDiscountsCommandValidator(
        IHttpContentResolver httpContentResolver,
        IEnumerable<IValidator<CreateDiscountRequest>> validators)
    {
        RuleFor(x => x.Request)
            .CustomAsync(async (request, context, _) =>
            {
                var body = await httpContentResolver.GetPayloadAsync<CreateDiscountRequest>(request);
                var validationResults = validators.Select(x => x.ValidateAsync(context.CloneForChildValidator(body)));

                await Task.WhenAll(validationResults);
            });
    }
}
