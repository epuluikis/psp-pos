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
        await _context.AddAsync(businessDao);
        await _context.SaveChangesAsync();
        return businessDao;
    }

    Task<IEnumerable<BusinessDao>> IBusinessesRepository.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    Task<BusinessDao> IBusinessesRepository.GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public void DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }
}
