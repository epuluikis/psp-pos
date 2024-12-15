using FluentValidation;
using Looms.PoS.Application.Interfaces.Factories;
using Looms.PoS.Application.Models.Requests.PaymentTerminal;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.PaymentTerminal.Commands.CreatePaymentTerminal;

public class CreatePaymentTerminalRequestValidator : AbstractValidator<CreatePaymentTerminalRequest>
{
    public CreatePaymentTerminalRequestValidator(
        IPaymentProvidersRepository paymentProvidersRepository,
        IPaymentProviderServiceFactory paymentProviderServiceFactory)
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.ExternalId)
            .NotEmpty();

        RuleFor(x => x.PaymentProviderId)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (id, _, _) => await paymentProvidersRepository.GetAsync(Guid.Parse(id!)));

        RuleFor(x => x.IsActive)
            .NotEmpty();

        RuleFor(x => x)
            .MustAsync(async (x, _, _) =>
            {
                var paymentProviderDao = await paymentProvidersRepository.GetAsync(Guid.Parse(x.PaymentProviderId));

                return await paymentProviderServiceFactory.GetService(paymentProviderDao.Type)
                                                          .ValidateTerminal(paymentProviderDao, x.ExternalId);
            });
    }
}
