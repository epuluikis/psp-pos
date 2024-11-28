using FluentValidation;
using Looms.PoS.Application.Models.Requests.Reservation;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Application.Utilities;
using Looms.PoS.Domain.Interfaces;
using Looms.PoS.Domain.Exceptions;

namespace Looms.PoS.Application.Features.Reservation.Commands.UpdateReservation;

public class UpdateReservationRequestValidator : AbstractValidator<UpdateReservationRequest>
{
     private readonly IServicesRepository _servicesRepository;
    public UpdateReservationRequestValidator(IServicesRepository servicesRepository)
    {    
        _servicesRepository = servicesRepository;
                   
        RuleFor(x => x.AppointmentTime)
            .Cascade(CascadeMode.Stop)
            .MustBeValidDateTime()
            .MustBeWithinBusinessHours()
            .Must(dateString =>
            {
                var parsedDate = DateTimeHelper.ConvertToUtc(dateString);
                return parsedDate >= DateTime.UtcNow;
            })
            .WithMessage("Appointment time must be in the future and within business hours.");

        RuleFor(x => x.ServiceId)
            .MustBeValidGuid()
            .MustAsync(async (serviceId, cancellation) => 
                {
                    if (Guid.TryParse(serviceId, out var guid))
                    {
                        try
                        {
                            await _servicesRepository.GetAsync(guid);
                            return true;
                        }
                        catch (LoomsNotFoundException)
                        {
                            return false;
                        }
                    }
                    return false;
                })
            .WithMessage("Service does not exist.");
        
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