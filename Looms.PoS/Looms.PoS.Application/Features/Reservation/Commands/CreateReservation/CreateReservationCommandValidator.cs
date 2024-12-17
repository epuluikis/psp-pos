using FluentValidation;
using Looms.PoS.Application.Interfaces;
using Looms.PoS.Application.Models.Requests.Reservation;

namespace Looms.PoS.Application.Features.Reservation.Commands.CreateReservation;

public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator(IHttpContentResolver httpContentResolver, IEnumerable<IValidator<CreateReservationRequest>> validators)
    {
        RuleFor(x => x.Request)
            .CustomAsync(async (request, context, _) =>
            {
                var body = await httpContentResolver.GetPayloadAsync<CreateReservationRequest>(request);
                var validationResults = validators.Select(x => x.ValidateAsync(context.CloneForChildValidator(body)));
                
                await Task.WhenAll(validationResults);
            });
    }
}
