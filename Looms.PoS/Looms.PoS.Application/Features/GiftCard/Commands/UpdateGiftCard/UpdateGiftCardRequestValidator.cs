﻿using FluentValidation;
using Looms.PoS.Application.Models.Requests.GiftCard;
using Looms.PoS.Application.Utilities;

namespace Looms.PoS.Application.Features.GiftCard.Commands.UpdateGiftCard;

public class UpdateGiftCardRequestValidator : AbstractValidator<UpdateGiftCardRequest>
{
    public UpdateGiftCardRequestValidator()
    {
        // TODO: validate code uniqueness per business
        RuleFor(x => x.Code)
            .NotEmpty();

        RuleFor(x => x.CurrentBalance)
            .NotEmpty()
            .PrecisionScale(10, 2, false)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.ExpiryDate)
            .NotEmpty()
            .Must(dateString =>
            {
                var parsedDate = DateTimeHelper.ConvertToUtc(dateString);
                return parsedDate >= DateTime.UtcNow;
            })
            .WithMessage("Start date must be a valid future date.");

        RuleFor(x => x.IsActive)
            .NotEmpty();
    }
}
