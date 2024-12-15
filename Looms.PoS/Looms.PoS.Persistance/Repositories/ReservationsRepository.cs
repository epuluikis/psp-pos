using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Persistance.Repositories;

public class ReservationsRepository : IReservationsRepository
{
    private readonly AppDbContext _context;

    public ReservationsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ReservationDao> CreateAsync(ReservationDao reservationDao)
    {
        var entityEntry = await _context.AddAsync(reservationDao);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<IEnumerable<ReservationDao>> GetAllAsync()
    {
        return await _context.Reservations.Where(x => !x.IsDeleted).ToListAsync();
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

    public async Task<IEnumerable<ReservationDao>> GetReservationsByCustomerAndTimeAsync(string customerName, DateTime appointmentTime)
    {
        return await _context.Reservations
            .Where(r => r.CustomerName == customerName && r.AppointmentTime == appointmentTime && !r.IsDeleted)
            .ToListAsync();
    }
    public async Task<IEnumerable<ReservationDao>> GetReservationsByEmployeeAndTimeAsync(Guid employeeId, DateTime appointmentTime)
    {
        return await _context.Reservations
            .Where(r => r.EmployeeId == employeeId && r.AppointmentTime == appointmentTime && !r.IsDeleted)
            .ToListAsync();
    }
}
