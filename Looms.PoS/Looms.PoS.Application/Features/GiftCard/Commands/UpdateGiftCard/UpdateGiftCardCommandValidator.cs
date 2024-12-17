using FluentValidation;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests.GiftCard;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.GiftCard.Commands.UpdateGiftCard;

public class UpdateGiftCardCommandValidator : AbstractValidator<UpdateGiftCardCommand>
{
    public UpdateGiftCardCommandValidator(
        IHttpContentResolver httpContentResolver,
        IEnumerable<IValidator<UpdateGiftCardRequest>> validators,
        IGiftCardsRepository giftCardsRepository)
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (id, context, _)
                => await giftCardsRepository.GetAsyncByIdAndBusinessId(
                    Guid.Parse(id!),
                    Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader])
                )
            );

        RuleFor(x => x)
            .CustomAsync(async (command, context, _) =>
            {
                context.RootContextData["Id"] = command.Id;

                var body = await httpContentResolver.GetPayloadAsync<UpdateGiftCardRequest>(command.Request);
                var validationResults = validators.Select(x => x.ValidateAsync(context.CloneForChildValidator(body)));

                await Task.WhenAll(validationResults);
            });
    }
}
