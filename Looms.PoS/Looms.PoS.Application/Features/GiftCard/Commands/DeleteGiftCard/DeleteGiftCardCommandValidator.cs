using FluentValidation;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.GiftCard.Commands.DeleteGiftCard;

public class DeleteGiftCardCommandValidator : AbstractValidator<DeleteGiftCardCommand>
{
    public DeleteGiftCardCommandValidator(
        IGiftCardsRepository giftCardsRepository)
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (id, _, _) => await giftCardsRepository.GetAsync(Guid.Parse(id)));
    }
}
