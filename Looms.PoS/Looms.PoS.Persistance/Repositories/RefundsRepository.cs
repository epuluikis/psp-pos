using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Persistance.Repositories;
public class RefundsRepository : IRefundsRepository
{
    private readonly AppDbContext _context;

    public RefundsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<RefundDao> CreateAsync(RefundDao refundDao)
    {
        var entityEntry = await _context.AddAsync(refundDao);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public IEnumerable<RefundDao> GetAll()
    {
        return _context.Refunds;
    }

    public void DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<RefundDao> GetAsync(string id)
    {
        throw new NotImplementedException();
    }
}
