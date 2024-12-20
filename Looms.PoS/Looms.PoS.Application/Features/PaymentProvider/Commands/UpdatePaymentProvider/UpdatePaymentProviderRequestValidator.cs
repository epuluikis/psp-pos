﻿using FluentValidation;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Interfaces.Factories;
using Looms.PoS.Application.Models.Requests.PaymentProvider;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.PaymentProvider.Commands.UpdatePaymentProvider;

public class UpdatePaymentProviderRequestValidator : AbstractValidator<UpdatePaymentProviderRequest>
{
    public UpdatePaymentProviderRequestValidator(
        IPaymentProvidersRepository paymentProvidersRepository,
        IPaymentProviderServiceFactory paymentProviderServiceFactory
    )
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Type)
            .NotNull()
            .IsInEnum();

        RuleFor(x => x.ExternalId)
            .NotEmpty();

        RuleFor(x => x.ApiSecret)
            .NotEmpty();

        RuleFor(x => x.WebhookSecret)
            .NotEmpty();

        RuleFor(x => x)
            .NotEmpty();

        RuleFor(x => x.IsActive)
            .NotNull()
            .MustAsync(async (_, isActive, context, _) =>
                !isActive || !await paymentProvidersRepository.ExistsActiveByBusinessIdExcludingId(
                    Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader]),
                    Guid.Parse((string)context.RootContextData["Id"])
                )
            ).WithMessage("Only a single active payment provider is allowed.");

        RuleFor(x => x)
            .MustAsync(async (x, _, _) =>
                await paymentProviderServiceFactory.GetService(x.Type).Validate(x.ExternalId, x.ApiSecret)
            );
    }
}
