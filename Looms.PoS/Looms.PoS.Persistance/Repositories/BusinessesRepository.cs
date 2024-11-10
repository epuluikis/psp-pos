using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Persistance.Repositories;

public class BusinessesRepository : IBusinessesRepository
{
    private readonly AppDbContext _context;

    public BusinessesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BusinessDao> CreateAsync(BusinessDao businessDao)
    {
        var entityEntry = await _context.AddAsync(businessDao);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<IEnumerable<BusinessDao>> GetAllAsync()
    {
        return await _context.Businesses.ToListAsync();
    }

    public async Task<BusinessDao> GetAsync(Guid id)
    {
        return await _context.Businesses.FindAsync(id)
            ?? throw new LoomsNotFoundException("Business not found");
    }

    public void DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
