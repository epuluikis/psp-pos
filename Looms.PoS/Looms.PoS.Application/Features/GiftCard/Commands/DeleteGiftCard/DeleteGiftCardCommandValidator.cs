using FluentValidation;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.GiftCard.Commands.DeleteGiftCard;

public class DeleteGiftCardCommandValidator : AbstractValidator<DeleteGiftCardCommand>
{
    public DeleteGiftCardCommandValidator(IGiftCardsRepository giftCardsRepository)
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
    }
}
