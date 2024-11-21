using FluentValidation;
using Looms.PoS.Application.Models.Requests.Reservation;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Application.Utilities;

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
            .MustBeWithinBusinessHours()
            .Must(dateString =>
            {
                var parsedDate = DateTimeHelper.ConvertToUtc(dateString);
                return parsedDate >= DateTime.UtcNow;
            });

        RuleFor(x => x.ServiceId)
            .MustBeValidGuid();
        
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
