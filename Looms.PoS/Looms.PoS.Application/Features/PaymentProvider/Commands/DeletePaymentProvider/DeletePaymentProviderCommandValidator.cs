using FluentValidation;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.PaymentProvider.Commands.DeletePaymentProvider;

public class DeletePaymentProviderCommandValidator : AbstractValidator<DeletePaymentProviderCommand>
{
    public DeletePaymentProviderCommandValidator(
        IPaymentProvidersRepository paymentProvidersRepository)
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (id, _, _) => await paymentProvidersRepository.GetAsync(Guid.Parse(id)));
    }
}
