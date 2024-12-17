using FluentValidation;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Interfaces.Factories;
using Looms.PoS.Application.Models.Requests.PaymentTerminal;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.PaymentTerminal.Commands.UpdatePaymentTerminal;

public class UpdatePaymentTerminalRequestValidator : AbstractValidator<UpdatePaymentTerminalRequest>
{
    public UpdatePaymentTerminalRequestValidator(
        IPaymentProvidersRepository paymentProvidersRepository,
        IPaymentProviderServiceFactory paymentProviderServiceFactory,
        IPaymentTerminalsRepository paymentTerminalsRepository)
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.ExternalId)
            .NotEmpty()
            .MustAsync(async (_, externalId, context, _) => 
            !await paymentTerminalsRepository.ExistsWithExternalIdAndBusinessIdExcludingId(
                externalId,
                Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader]),
                Guid.Parse((string)context.RootContextData["Id"])
            )
        );

        RuleFor(x => x.PaymentProviderId)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (id, _, _) => await paymentProvidersRepository.GetAsync(Guid.Parse(id!)));

        RuleFor(x => x.IsActive)
            .NotNull();

        RuleFor(x => x)
            .MustAsync(async (x, _, _) =>
            {
                var paymentProviderDao = await paymentProvidersRepository.GetAsync(Guid.Parse(x.PaymentProviderId));

                return await paymentProviderServiceFactory.GetService(paymentProviderDao.Type)
                                                          .ValidateTerminal(paymentProviderDao, x.ExternalId);
            });
    }
}
