using FluentValidation;
using Looms.PoS.Application.Utilities.Validators;

namespace Looms.PoS.Application.Features.PaymentProvider.Queries.GetPaymentProvider;

public class GetPaymentProviderQueryValidator : AbstractValidator<GetPaymentProviderQuery>
{
    public GetPaymentProviderQueryValidator()
    {
        RuleFor(x => x.Id).MustBeValidGuid();
    }
}
