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
            .CustomAsync(async (request, context, cancellationToken) =>
            {
                CreatePaymentRequest body = await httpContentResolver.GetPayloadAsync<CreatePaymentRequest>(request);

                IEnumerable<Task<ValidationResult>> validationResults = validators.Select(x => x.ValidateAsync(body));
                await Task.WhenAll(validationResults);

                foreach (ValidationFailure? validationError in validationResults.SelectMany(x => x.Result.Errors))
                {
                    context.AddFailure(validationError);
                }
            });
    }
}
