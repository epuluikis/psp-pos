using FluentValidation;
using Looms.PoS.Application.Utilities.Validators;

namespace Looms.PoS.Application.Features.PaymentTerminal.Queries.GetPaymentTerminal;

public class GetPaymentTerminalQueryValidator : AbstractValidator<GetPaymentTerminalQuery>
{
    public GetPaymentTerminalQueryValidator()
    {
        RuleFor(x => x.Id).MustBeValidGuid();
    }
}
