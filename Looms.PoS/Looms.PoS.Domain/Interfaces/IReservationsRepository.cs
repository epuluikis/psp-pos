using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IReservationsRepository
{
    Task<ReservationDao> CreateAsync(ReservationDao reservationDao);
    Task<IEnumerable<ReservationDao>> GetAllAsync();
    Task<ReservationDao> GetAsync(Guid id);
    Task<ReservationDao> UpdateAsync(ReservationDao reservationDao);
    Task<bool> ExistsWithTimeOverlapAndCustomer(DateTime start, DateTime end, string customerName, string email);
    Task<bool> ExistsWithTimeOverlapAndEmployeeId(DateTime start, DateTime end, Guid employeeId);
}
