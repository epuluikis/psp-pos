using FluentValidation;
using Looms.PoS.Application.Models.Requests.Reservation;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Application.Utilities;
using Looms.PoS.Domain.Interfaces;
using Looms.PoS.Domain.Exceptions;

namespace Looms.PoS.Application.Features.Reservation.Commands.CreateReservation;

public class CreateReservationRequestValidator : AbstractValidator<CreateReservationRequest>
{
    private readonly IServicesRepository _servicesRepository;
    private readonly IReservationsRepository _reservationsRepository;
    public CreateReservationRequestValidator(IServicesRepository servicesRepository, IReservationsRepository reservationsRepository)
    {
        _servicesRepository = servicesRepository;
        _reservationsRepository = reservationsRepository;

        RuleFor(x => x.CustomerId)
            .MustBeValidGuid()
            .MustAsync(async (request, customerId, cancellation) =>
                {
                    if (Guid.TryParse(customerId, out var customerGuid))
                    {
                        var appointmentTime = DateTimeHelper.ConvertToUtc(request.AppointmentTime);
                        var allReservations = await _reservationsRepository.GetAllAsync();
                        var existingReservation = allReservations
                            .FirstOrDefault(r => r.CustomerId == customerGuid && r.AppointmentTime == appointmentTime);
                        return existingReservation == null;
                    }
                    return false;
                })
                .WithMessage("An appointment for the same customer at the same time already exists.");
                    
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
