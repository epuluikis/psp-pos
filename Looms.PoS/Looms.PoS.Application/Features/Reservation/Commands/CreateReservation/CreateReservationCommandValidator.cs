using FluentValidation;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests.Reservation;

namespace Looms.PoS.Application.Features.Reservation.Commands.CreateReservation;

public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator(IHttpContentResolver httpContentResolver, IEnumerable<IValidator<CreateReservationRequest>> validators)
    {
        RuleFor(x => x.Request)
            .CustomAsync(async (request, context, cancellationToken) =>
            {
                var body = await httpContentResolver.GetPayloadAsync<CreateReservationRequest>(request);

                var validationResults = validators.Select(x => x.ValidateAsync(body));
                await Task.WhenAll(validationResults);

                foreach (var validationError in validationResults.SelectMany(x => x.Result.Errors))
                {
                    context.AddFailure(validationError);
                }
            });
    }
}
