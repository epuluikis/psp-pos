using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Application.Options;
using Looms.PoS.Application.Utilities.Helpers;
using Looms.PoS.Domain.Daos;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Looms.PoS.Application.Services.Notification;

public class TwilioNotificationService : INotificationService
{
    private readonly TwilioOptions _options;

    public TwilioNotificationService(IOptions<TwilioOptions> options)
    {
        _options = options.Value;

        TwilioClient.Init(_options.AccountSid, _options.AuthToken);
    }

    public async Task SendReservationNotification(ReservationDao reservationDao)
    {
        await MessageResource.CreateAsync(
            body:
            $"Hello {reservationDao.CustomerName}, your reservation at {reservationDao.Service.Business.Name} for {reservationDao.Service.Name} with {reservationDao.Employee.Name} is confirmed for {reservationDao.AppointmentTime.ToString(DateTimeHelper.DateFormat)}. We look forward to seeing you!",
            from: new Twilio.Types.PhoneNumber(_options.FromNumber),
            to: new Twilio.Types.PhoneNumber(reservationDao.PhoneNumber)
        );
    }
}
