using FluentValidation;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Models.Requests.GiftCard;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.GiftCard.Commands.CreateGiftCard;

public class CreateGiftCardRequestValidator : AbstractValidator<CreateGiftCardRequest>
{
    public CreateGiftCardRequestValidator(IGiftCardsRepository giftCardsRepository)
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MustAsync(async (_, code, context, _) =>
                !await giftCardsRepository.ExistsAsyncWithCodeAndBusinessId(code,
                    Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader])
                )
            );

        RuleFor(x => x.InitialBalance)
            .NotEmpty()
            .PrecisionScale(10, 2, false)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ExpiryDate)
            .MustBeValidDateTime();

        RuleFor(x => x.IsActive)
            .NotNull();
    }
}
