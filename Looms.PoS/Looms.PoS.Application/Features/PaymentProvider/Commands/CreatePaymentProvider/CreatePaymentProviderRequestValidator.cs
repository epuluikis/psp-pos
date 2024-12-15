using FluentValidation;
using Looms.PoS.Application.Interfaces.Factories;
using Looms.PoS.Application.Models.Requests.PaymentProvider;
using Looms.PoS.Application.Utilities;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.PaymentProvider.Commands.CreatePaymentProvider;

public class CreatePaymentProviderRequestValidator : AbstractValidator<CreatePaymentProviderRequest>
{
    public CreatePaymentProviderRequestValidator(
        IPaymentProvidersRepository paymentProvidersRepository,
        IPaymentProviderServiceFactory paymentProviderServiceFactory
    )
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Type)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .IsInEnum();

        RuleFor(x => x.ExternalId)
            .NotEmpty();

        RuleFor(x => x.ApiSecret)
            .NotEmpty();

        RuleFor(x => x.WebhookSecret)
            .NotEmpty();

        RuleFor(x => x.IsActive)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .MustAsync(async (isActive, _) =>
                // TODO: add legit business id
                !isActive || !await paymentProvidersRepository.ExistsActiveByBusinessId(Guid.NewGuid())
            ).WithMessage("Only a single active payment provider is allowed.");

        RuleFor(x => x)
            .Cascade(CascadeMode.Stop)
            .MustAsync(async (x, _, _) =>
                await paymentProviderServiceFactory.GetService(x.Type).Validate(x.ExternalId, x.ApiSecret)
            );
    }
}
