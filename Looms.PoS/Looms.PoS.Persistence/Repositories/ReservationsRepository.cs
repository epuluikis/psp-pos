using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Persistence.Repositories;

public class ReservationsRepository : IReservationsRepository
{
    private readonly AppDbContext _context;

    public ReservationsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ReservationDao> CreateAsync(ReservationDao reservationDao)
    {
        reservationDao = _context.CreateProxy<ReservationDao>(reservationDao);

        await _context.AddAsync(reservationDao);
        await _context.SaveChangesAsync();

        return reservationDao;
    }

    public async Task<IEnumerable<ReservationDao>> GetAllAsync()
    {
        return await _context.Reservations.Where(x => !x.IsDeleted).ToListAsync();
    }

    public async Task<IEnumerable<ReservationDao>> GetALlAsyncByBusinessId(Guid businessId)
    {
        return await _context.Reservations
            .Where(x => !x.IsDeleted && x.Service.BusinessId == businessId)
            .ToListAsync();
    }

    public async Task<ReservationDao> GetAsyncByIdAndBusinessId(Guid id, Guid businessId)
    {
        var reservationDao = await _context.Reservations
            .Where(x => x.Id == id && x.Service.BusinessId == businessId)
            .FirstOrDefaultAsync();

        if (reservationDao is null || reservationDao.IsDeleted)
        {
            throw new LoomsNotFoundException("Reservation not found");
        }

        return reservationDao;
    }


    public async Task<ReservationDao> GetAsync(Guid id)
    {
        var reservationDao = await _context.Reservations.FindAsync(id);

        if (reservationDao is null || reservationDao.IsDeleted)
        {
            throw new LoomsNotFoundException("Reservation not found");
        }

        return reservationDao;
    }

    public async Task<ReservationDao> UpdateAsync(ReservationDao reservationDao)
    {
        await RemoveAsync(reservationDao.Id);
        _context.Reservations.Update(reservationDao);
        await _context.SaveChangesAsync();

        return reservationDao;
    }

    private async Task RemoveAsync(Guid id)
    {
        var reservationDao = await _context.Reservations.FindAsync(id);
        _context.Reservations.Remove(reservationDao!);
    }

    public async Task<bool> ExistsWithTimeOverlapAndCustomer(DateTime start, DateTime end, string customerName, string email)
    {
        return await _context.Reservations.AnyAsync(r
            => r.CustomerName == customerName
            && r.Email == email
            && start < r.AppointmentTime.AddMinutes(r.Service.DurationMin)
            && r.AppointmentTime < end);
    }

    public async Task<bool> ExistsWithTimeOverlapAndEmployeeId(DateTime start, DateTime end, Guid employeeId)
    {
        return await _context.Reservations.AnyAsync(r
            => r.EmployeeId == employeeId && start < r.AppointmentTime.AddMinutes(r.Service.DurationMin) && r.AppointmentTime < end);
    }
}
