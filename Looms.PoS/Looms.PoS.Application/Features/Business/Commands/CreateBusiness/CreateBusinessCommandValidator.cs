using FluentValidation;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests.Business;

namespace Looms.PoS.Application.Features.Business.Commands.CreateBusiness;

public class CreateBusinessCommandValidator : AbstractValidator<CreateBusinessCommand>
{
    public CreateBusinessCommandValidator(IHttpContentResolver httpContentResolver, IEnumerable<IValidator<CreateBusinessRequest>> validators)
    {
        RuleFor(x => x.Request)
            .CustomAsync(async (request, context, _) =>
            {
                var body = await httpContentResolver.GetPayloadAsync<CreateBusinessRequest>(request);
                var validationResults = validators.Select(x => x.ValidateAsync(context.CloneForChildValidator(body)));

                await Task.WhenAll(validationResults);
            });
    }
}
