using FluentValidation;
using Looms.PoS.Application.Models.Requests.Reservation;
using Looms.PoS.Application.Utilities.Validators;
using Looms.PoS.Application.Utilities;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Features.Reservation.Commands.CreateReservation;

public class CreateReservationRequestValidator : AbstractValidator<CreateReservationRequest>
{
    public CreateReservationRequestValidator(IServicesRepository servicesRepository, IReservationsRepository reservationsRepository)
    {
        RuleFor(x => x.CustomerName)
            .Cascade(CascadeMode.Stop)
            .MustAsync(async (request, customerName, cancellation) =>
            {
                var appointmentTime = DateTimeHelper.ConvertToUtc(request.AppointmentTime);
                var existingReservations = await reservationsRepository.GetReservationsByCustomerAndTimeAsync(customerName, request.Email, appointmentTime);
                var existingReservation = existingReservations.FirstOrDefault();
                return existingReservation is null;
            })
            .WithMessage("An appointment for the same customer at the same time already exists.");

        RuleFor(x => x.AppointmentTime)
            .Cascade(CascadeMode.Stop)
            .MustBeValidDateTime()
            .MustAsync(async (request, dateString, cancellation) =>
            {
                var service = await servicesRepository.GetAsync(Guid.Parse(request.ServiceId));
                var business = service.Business;
                var appointmentTime = DateTimeHelper.ConvertToUtc(dateString);
                var endTime = appointmentTime.AddMinutes(service.DurationMin);
                return appointmentTime.Hour >= business.StartHour && endTime.Hour < business.EndHour;
            })
            .Must(dateString =>
            {
                var parsedDate = DateTimeHelper.ConvertToUtc(dateString);
                return parsedDate >= DateTime.UtcNow;
            })
            .WithMessage("Appointment time must be in the future and within business hours.");

        RuleFor(x => x.EmployeeId)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .MustAsync(async (request, employeeId, cancellation) =>
            {
                var employeeGuid = Guid.Parse(employeeId);
                var appointmentTime = DateTimeHelper.ConvertToUtc(request.AppointmentTime);
                var existingReservations = await reservationsRepository.GetReservationsByEmployeeAndTimeAsync(employeeGuid, appointmentTime);
                var existingReservation = existingReservations.FirstOrDefault();
                return existingReservation is null;
            })
            .WithMessage("An appointment with the same employee at the same time already exists.");
            

        RuleFor(x => x.ServiceId)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (serviceId, context, cancellation) => 
            {
                await servicesRepository.GetAsync(new Guid(serviceId));
            });
        
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