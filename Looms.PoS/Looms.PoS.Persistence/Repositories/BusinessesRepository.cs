using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Persistence.Repositories;

public class BusinessesRepository : IBusinessesRepository
{
    private readonly AppDbContext _context;

    public BusinessesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BusinessDao> CreateAsync(BusinessDao businessDao)
    {
        businessDao = _context.CreateProxy<BusinessDao>(businessDao);

        var entityEntry = await _context.AddAsync(businessDao);
        await _context.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task<IEnumerable<BusinessDao>> GetAllAsync()
    {
        return await _context.Businesses.Where(x => !x.IsDeleted).ToListAsync();
    }

    public async Task<BusinessDao> GetAsync(Guid id)
    {
        var businessDao = await _context.Businesses.FindAsync(id);

        if (businessDao is null || businessDao.IsDeleted)
        {
            throw new LoomsNotFoundException("Business not found");
        }

        return businessDao;
    }

    public async Task<BusinessDao> UpdateAsync(BusinessDao businessDao)
    {
        await RemoveAsync(businessDao.Id);
        _context.Businesses.Update(businessDao);
        await _context.SaveChangesAsync();

        return businessDao;
    }

    private async Task RemoveAsync(Guid id)
    {
        var businessDao = await _context.Businesses.FindAsync(id);
        _context.Businesses.Remove(businessDao!);
    }
}
