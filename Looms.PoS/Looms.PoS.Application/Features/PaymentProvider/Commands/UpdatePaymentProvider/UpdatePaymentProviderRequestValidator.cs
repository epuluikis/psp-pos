using FluentValidation;
using Looms.PoS.Application.Interfaces.Factories;
using Looms.PoS.Application.Models.Requests.PaymentProvider;
using Looms.PoS.Application.Utilities;
using Looms.PoS.Domain.Exceptions;
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

        // TODO: allow single active per business
        // RuleFor(x => x.IsActive)
        //     .NotEmpty()
        //     .MustAsync(async (isActive, _, context, _) =>
        //         !isActive || !await paymentProvidersRepository.ExistsActiveByBusinessIdExcludingId(Guid.NewGuid())
        //     ).WithMessage("Only a single active payment provider is allowed.");

        RuleFor(x => x)
            .MustAsync(async (x, _, _) =>
                await paymentProviderServiceFactory.GetService(x.Type).Validate(x.ExternalId, x.ApiSecret)
            );
    }
}
