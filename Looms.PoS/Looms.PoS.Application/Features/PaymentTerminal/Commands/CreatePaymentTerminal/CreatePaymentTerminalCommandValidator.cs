using FluentValidation;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests.PaymentTerminal;

namespace Looms.PoS.Application.Features.PaymentTerminal.Commands.CreatePaymentTerminal;

public class CreatePaymentTerminalCommandValidator : AbstractValidator<CreatePaymentTerminalCommand>
{
    public CreatePaymentTerminalCommandValidator(
        IHttpContentResolver httpContentResolver,
        IEnumerable<IValidator<CreatePaymentTerminalRequest>> validators)
    {
        RuleFor(x => x.Request)
            .CustomAsync(async (request, context, cancellationToken) =>
            {
                var body = await httpContentResolver.GetPayloadAsync<CreatePaymentTerminalRequest>(request);
                var validationResults = validators.Select(x => x.ValidateAsync(context.CloneForChildValidator(body)));
                
                await Task.WhenAll(validationResults);
            });
    }
}
