using FluentValidation;
using FluentValidation.Results;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests.Payment;

namespace Looms.PoS.Application.Features.Payment.Commands.CreatePayment;

public class CreatePaymentCommandValidator : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentCommandValidator(IHttpContentResolver httpContentResolver, IEnumerable<IValidator<CreatePaymentRequest>> validators)
    {
        RuleFor(x => x.Request)
            .CustomAsync(async (request, context, _) =>
            {
                var body = await httpContentResolver.GetPayloadAsync<CreatePaymentRequest>(request);
                var validationResults = validators.Select(x => x.ValidateAsync(context.CloneForChildValidator(body)));

                await Task.WhenAll(validationResults);
            });
    }
}
