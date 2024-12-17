using FluentValidation;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests.GiftCard;

namespace Looms.PoS.Application.Features.GiftCard.Commands.CreateGiftCard;

public class CreateGiftCardCommandValidator : AbstractValidator<CreateGiftCardCommand>
{
    public CreateGiftCardCommandValidator(IHttpContentResolver httpContentResolver, IEnumerable<IValidator<CreateGiftCardRequest>> validators)
    {
        RuleFor(x => x.Request)
            .CustomAsync(async (request, context, _) =>
            {
                var body = await httpContentResolver.GetPayloadAsync<CreateGiftCardRequest>(request);

                var validationResults = validators.Select(x => x.ValidateAsync(body));
                await Task.WhenAll(validationResults);

                foreach (var validationError in validationResults.SelectMany(x => x.Result.Errors))
                {
                    context.AddFailure(validationError);
                }
            });
    }
}
