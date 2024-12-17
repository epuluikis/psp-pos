using FluentValidation;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests.PaymentTerminal;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.PaymentTerminal.Commands.UpdatePaymentTerminal;

public class UpdatePaymentTerminalCommandValidator : AbstractValidator<UpdatePaymentTerminalCommand>
{
    public UpdatePaymentTerminalCommandValidator(
        IHttpContentResolver httpContentResolver,
        IEnumerable<IValidator<UpdatePaymentTerminalRequest>> validators,
        IPaymentTerminalsRepository paymentTerminalsRepository)
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (id, context, _) 
                => await paymentTerminalsRepository.GetAsyncByIdAndBusinessId(
                    Guid.Parse(id!),
                    Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader])
                ));

        RuleFor(x => x)
            .CustomAsync(async (command, context, _) =>
            {
                context.RootContextData["Id"] = command.Id;

                var body = await httpContentResolver.GetPayloadAsync<UpdatePaymentTerminalRequest>(command.Request);
                var validationResults = validators.Select(x => x.ValidateAsync(context.CloneForChildValidator(body)));
                
                await Task.WhenAll(validationResults);
            });
    }
}
