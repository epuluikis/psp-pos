using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.Services;

public interface INotificationService
{
    Task SendReservationNotification(ReservationDao reservationDao);
}
