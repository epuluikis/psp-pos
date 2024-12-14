using FluentValidation;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests.PaymentProvider;

namespace Looms.PoS.Application.Features.PaymentProvider.Commands.CreatePaymentProvider;

public class CreatePaymentProviderCommandValidator : AbstractValidator<CreatePaymentProviderCommand>
{
    public CreatePaymentProviderCommandValidator(
        IHttpContentResolver httpContentResolver,
        IEnumerable<IValidator<CreatePaymentProviderRequest>> validators)
    {
        RuleFor(x => x.Request)
            .CustomAsync(async (request, context, cancellationToken) =>
            {
                var body = await httpContentResolver.GetPayloadAsync<CreatePaymentProviderRequest>(request);

                var validationResults = validators.Select(x => x.ValidateAsync(body));
                await Task.WhenAll(validationResults);

                foreach (var validationError in validationResults.SelectMany(x => x.Result.Errors))
                {
                    context.AddFailure(validationError);
                }
            });
    }
}
