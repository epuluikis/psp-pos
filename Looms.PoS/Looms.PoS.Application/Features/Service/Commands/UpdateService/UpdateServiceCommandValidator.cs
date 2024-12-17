using FluentValidation;
using Looms.PoS.Application.Constants;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests.Service;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Service.Commands.UpdateService;

public class UpdateServiceCommandValidator : AbstractValidator<UpdateServiceCommand>
{
    public UpdateServiceCommandValidator(
        IHttpContentResolver httpContentResolver,
        IEnumerable<IValidator<UpdateServiceRequest>> validators,
        IServicesRepository servicesRepository)
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (id, context, _) 
                => await servicesRepository.GetAsyncByIdAndBusinessId(
                    Guid.Parse(id!),
                    Guid.Parse((string)context.RootContextData[HeaderConstants.BusinessIdHeader])
                )
            );

        RuleFor(x => x)
            .CustomAsync(async (command, context, _) =>
            {
                var body = await httpContentResolver.GetPayloadAsync<UpdateServiceCommand>(command.Request);
                var validationResults = validators.Select(x => x.ValidateAsync(context.CloneForChildValidator(body)));
                
                await Task.WhenAll(validationResults);
            });
    }
}