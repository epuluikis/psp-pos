using FluentValidation;
using Looms.PoS.Application.Utilities.Validators;

namespace Looms.PoS.Application.Features.GiftCard.Queries.GetGiftCard;

public class GetGiftCardQueryValidator : AbstractValidator<GetGiftCardQuery>
{
    public GetGiftCardQueryValidator()
    {
        RuleFor(x => x.Id).MustBeValidGuid();
    }
}
