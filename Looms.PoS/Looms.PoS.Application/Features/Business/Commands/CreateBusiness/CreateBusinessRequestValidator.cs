﻿using FluentValidation;
using Looms.PoS.Application.Models.Requests.Business;
using Looms.PoS.Application.Utilities.Validators;

namespace Looms.PoS.Application.Features.Business.Commands.CreateBusiness;

public class CreateBusinessRequestValidator : AbstractValidator<CreateBusinessRequest>
{
    public CreateBusinessRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Owner)
            .NotEmpty();

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress();

        When(x => x.PhoneNumber is not null, () =>
        {
            RuleFor(x => x.PhoneNumber!)
                .MustBeValidPhoneNumber();
        });

        RuleFor(x => x.StartHour)
            .Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(24)
            .LessThan(x => x.EndHour);

        RuleFor(x => x.EndHour)
            .Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(24)
            .GreaterThan(x => x.StartHour);
    }
}
