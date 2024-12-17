using FluentValidation;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests.PaymentProvider;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.PaymentProvider.Commands.UpdatePaymentProvider;

public class UpdatePaymentProviderCommandValidator : AbstractValidator<UpdatePaymentProviderCommand>
{
    public UpdatePaymentProviderCommandValidator(
        IHttpContentResolver httpContentResolver,
        IEnumerable<IValidator<UpdatePaymentProviderRequest>> validators,
        IPaymentProvidersRepository paymentProvidersRepository)
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (id, context, _) 
                => await paymentProvidersRepository.GetAsyncByIdAndBusinessId(
                    Guid.Parse(id!),
                    Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader])
                    )
            );

        RuleFor(x => x)
            .CustomAsync(async (command, context, _) =>
            {
                context.RootContextData["Id"] = command.Id;

                var body = await httpContentResolver.GetPayloadAsync<UpdatePaymentProviderRequest>(command.Request);
                var validationResults = validators.Select(x => x.ValidateAsync(context.CloneForChildValidator(body)));
                
                await Task.WhenAll(validationResults);
            });
    }
}
