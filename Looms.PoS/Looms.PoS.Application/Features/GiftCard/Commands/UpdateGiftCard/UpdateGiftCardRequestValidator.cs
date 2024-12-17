using FluentValidation;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Models.Requests.GiftCard;
using Looms.PoS.Application.Utilities.Helpers;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.GiftCard.Commands.UpdateGiftCard;

public class UpdateGiftCardRequestValidator : AbstractValidator<UpdateGiftCardRequest>
{
    public UpdateGiftCardRequestValidator(IGiftCardsRepository giftCardsRepository)
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MustAsync(async (_, code, context, _) =>
                !await giftCardsRepository.ExistsAsyncWithCodeAndBusinessIdExcludingId(
                    code,
                    Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader]),
                    Guid.Parse((string)context.RootContextData["Id"])
                )
            );

        RuleFor(x => x.CurrentBalance)
            .NotEmpty()
            .PrecisionScale(10, 2, false)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ExpiryDate)
            .MustBeValidDateTime();

        RuleFor(x => x.IsActive)
            .NotEmpty();
    }
}
