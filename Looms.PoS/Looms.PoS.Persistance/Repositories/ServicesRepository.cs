using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Persistance.Repositories;

public class ServicesRepository : IServicesRepository
{
    private readonly AppDbContext _context;

    public ServicesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceDao> CreateAsync(ServiceDao serviceDao)
    {
        serviceDao = _context.CreateProxy<ServiceDao>(serviceDao);

        var entityEntry = await _context.AddAsync(serviceDao);
        await _context.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task<IEnumerable<ServiceDao>> GetAllAsync()
    {
        return await _context.Services.ToListAsync();
    }

    public async Task<IEnumerable<ServiceDao>> GetAllAsyncByBusinessId(Guid businessId)
    {
        return await _context.Services
            .Where(x => x.BusinessId == businessId && !x.IsDeleted)
            .ToListAsync();
    }

    public async Task<ServiceDao> GetAsync(Guid id)
    {
        var serviceDao = await _context.Services.FindAsync(id);

        if (serviceDao is null || serviceDao.IsDeleted)
        {
            throw new LoomsNotFoundException("Service not found");
        }

        return serviceDao;
    }

    public async Task<ServiceDao> GetAsyncByIdAndBusinessId(Guid id, Guid businessId)
    {
        var serviceDao = await _context.Services
            .Where(x => x.Id == id && x.BusinessId == businessId)
            .FirstOrDefaultAsync();

        if (serviceDao is null || serviceDao.IsDeleted)
        {
            throw new LoomsNotFoundException("Service not found");
        }

        return serviceDao;
    }

    public async Task<ServiceDao> UpdateAsync(ServiceDao serviceDao)
    {
        await RemoveAsync(serviceDao.Id);
        _context.Services.Update(serviceDao);
        await _context.SaveChangesAsync();

        return serviceDao;
    }

    private async Task RemoveAsync(Guid id)
    {
        var serviceDao = await _context.Services.FindAsync(id);
        _context.Services.Remove(serviceDao!);
    }
}
