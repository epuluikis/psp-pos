using FluentValidation;
using Looms.PoS.Application.Models.Requests;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Persistance.Repositories;

namespace Looms.PoS.Application.Features.Reservation.Commands.CreateReservation;

public class CreateReservationRequestValidator : AbstractValidator<CreateReservationRequest>
{
    public CreateReservationRequestValidator()
    {
        RuleFor(x => x.CustomerId)
            .MustBeValidGuid();
                    
        RuleFor(x => x.AppointmentTime)
            .Cascade(CascadeMode.Stop)
            .MustBeValidDateTime()
            .MustBeWithinBusinessHours();

            
        RuleFor(x => x.ServiceId)
            .MustBeValidGuid()
            .ServiceExistAsync();
        
        RuleFor(x => x.PhoneNumber)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Matches(@"^\+?\d{1,3}?[-.\s]?\(?\d{1,4}?\)?[-.\s]?\d{1,4}[-.\s]?\d{1,9}$");
        
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress();
    }
}
