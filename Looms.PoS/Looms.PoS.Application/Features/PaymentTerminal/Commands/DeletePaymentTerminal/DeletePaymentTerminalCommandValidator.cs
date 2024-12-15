using FluentValidation;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.PaymentTerminal.Commands.DeletePaymentTerminal;

public class DeletePaymentTerminalCommandValidator : AbstractValidator<DeletePaymentTerminalCommand>
{
    public DeletePaymentTerminalCommandValidator(
        IPaymentTerminalsRepository paymentTerminalsRepository)
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (id, _, _) => await paymentTerminalsRepository.GetAsync(Guid.Parse(id!)));
    }
}
