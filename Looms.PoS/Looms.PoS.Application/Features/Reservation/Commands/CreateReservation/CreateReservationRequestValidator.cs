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
            .MustAsync(async (request, customerName, _) =>
            {
                var serviceDao = await servicesRepository.GetAsync(Guid.Parse(request.ServiceId));
                var appointmentTime = DateTimeHelper.ConvertToUtc(request.AppointmentTime);

                return !await reservationsRepository.ExistsWithTimeOverlapAndCustomer(
                    appointmentTime,
                    appointmentTime.AddMinutes(serviceDao.DurationMin),
                    customerName,
                    request.Email
                );
            })
            .WithMessage("An appointment for the same customer at the same time already exists.");

        RuleFor(x => x.AppointmentTime)
            .Cascade(CascadeMode.Stop)
            .MustBeValidDateTime()
            .Must(dateString => DateTimeHelper.ConvertToUtc(dateString) >= DateTime.UtcNow)
            .MustAsync(async (request, dateString, _) =>
            {
                var service = await servicesRepository.GetAsync(Guid.Parse(request.ServiceId));
                var business = service.Business;
                var appointmentTime = DateTimeHelper.ConvertToUtc(dateString);
                var endTime = appointmentTime.AddMinutes(service.DurationMin);
                return appointmentTime.Hour >= business.StartHour && endTime.Hour < business.EndHour;
            })
            .WithMessage("Appointment time must be in the future and within business hours.");

        RuleFor(x => x.EmployeeId)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .MustAsync(async (request, employeeId, _) =>
            {
                var serviceDao = await servicesRepository.GetAsync(Guid.Parse(request.ServiceId));
                var appointmentTime = DateTimeHelper.ConvertToUtc(request.AppointmentTime);

                return !await reservationsRepository.ExistsWithTimeOverlapAndEmployeeId(
                    appointmentTime,
                    appointmentTime.AddMinutes(serviceDao.DurationMin),
                    Guid.Parse(employeeId)
                );
            })
            .WithMessage("An appointment with the same employee at the same time already exists.");


        RuleFor(x => x.ServiceId)
            .Cascade(CascadeMode.Stop)
            .MustBeValidGuid()
            .CustomAsync(async (serviceId, _, _) =>
            {
                await servicesRepository.GetAsync(Guid.Parse(serviceId));
            });

        RuleFor(x => x.PhoneNumber)
            .Cascade(CascadeMode.Stop)
            .MustBeValidPhoneNumber();

        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .EmailAddress();
    }
}
