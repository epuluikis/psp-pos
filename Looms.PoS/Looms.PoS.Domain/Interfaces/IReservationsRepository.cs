using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IReservationsRepository
{
    Task<ReservationDao> CreateAsync(ReservationDao reservationDao);
    Task<IEnumerable<ReservationDao>> GetAllAsync();
    Task<ReservationDao> GetAsync(Guid id);
    void DeleteAsync(Guid id);
}
