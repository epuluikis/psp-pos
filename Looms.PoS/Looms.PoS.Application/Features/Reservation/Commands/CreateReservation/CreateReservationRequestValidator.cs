using FluentValidation;
using Looms.PoS.Application.Models.Requests;

namespace Looms.PoS.Application.Features.Reservation.Commands.CreateReservation;

public class CreateReservationRequestValidator : AbstractValidator<CreateReservationRequest>
{
    public CreateReservationRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.Owner)
            .NotEmpty();

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress();
    }
}
