using FluentValidation;
using Looms.PoS.Application.Utilities.Validators;

namespace Looms.PoS.Application.Features.Reservation.Queries.GetReservation;

public class GetReservationQueryValidator : AbstractValidator<GetReservationQuery>
{
    public GetReservationQueryValidator()
    {
        RuleFor(x => x.Id).MustBeValidGuid();
    }
}
