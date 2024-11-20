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
        var entityEntry = await _context.AddAsync(serviceDao);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<IEnumerable<ServiceDao>> GetAllAsync()
    {
        return await _context.Services.ToListAsync();
    }

    public async Task<ServiceDao> GetAsync(Guid id)
    {
        return await _context.Services.FindAsync(id)
            ?? throw new LoomsNotFoundException("Service not found");
    }
    
    public void DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
