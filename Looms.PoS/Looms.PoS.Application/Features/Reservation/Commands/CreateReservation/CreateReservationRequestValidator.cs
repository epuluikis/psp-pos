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
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .MustAsync(async (request, customerId, cancellation) =>
            {
                var customerGuid = Guid.Parse(customerId);
                var appointmentTime = DateTimeHelper.ConvertToUtc(request.AppointmentTime);
                var existingReservations = await _reservationsRepository.GetReservationsByCustomerAndTimeAsync(customerGuid, appointmentTime);
                var existingReservation = existingReservations.FirstOrDefault();
                return existingReservation is null;
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
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .MustAsync(async (serviceId, cancellation) => 
            {
                return await _servicesRepository.GetAsync(new Guid(serviceId)) != null;
            })
            .WithMessage("Invalid service ID.");
        
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