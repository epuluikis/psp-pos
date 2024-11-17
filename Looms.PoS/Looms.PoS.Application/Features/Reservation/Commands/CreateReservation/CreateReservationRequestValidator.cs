using FluentValidation;
using Looms.PoS.Application.Models.Requests;

namespace Looms.PoS.Application.Features.Reservation.Commands.CreateReservation;

public class CreateReservationRequestValidator : AbstractValidator<CreateReservationRequest>
{
    public CreateReservationRequestValidator()
    {

        RuleFor(x => x.CustomerId)
            .NotEmpty();
                    
        RuleFor(x => x.AppointmentTime)
            .NotEmpty();
            
        RuleFor(x => x.ServiceId)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .NotEqual(Guid.Empty);
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty();
        
        RuleFor(x => x.Email)
            .NotEmpty();


    }
}
