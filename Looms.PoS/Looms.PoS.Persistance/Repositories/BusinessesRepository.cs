using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Interfaces;

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

    public IEnumerable<BusinessDao> GetAll()
    {
        return _context.Businesses;
    }

    public Task<BusinessDao> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public void DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }
}
