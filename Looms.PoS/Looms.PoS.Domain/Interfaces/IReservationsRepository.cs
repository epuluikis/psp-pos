using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IReservationsRepository
{
    Task<ReservationDao> CreateAsync(ReservationDao reservationDao);
    Task<IEnumerable<ReservationDao>> GetAllAsync();
    Task<ReservationDao> GetAsync(Guid id);
    Task<ReservationDao> UpdateAsync(ReservationDao reservationDao);
    Task<IEnumerable<ReservationDao>> GetReservationsByCustomerAndTimeAsync(string customerName, string email, DateTime appointmentTime);
    Task<IEnumerable<ReservationDao>> GetReservationsByEmployeeAndTimeAsync(Guid employeeId, DateTime appointmentTime);
}
