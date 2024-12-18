using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        refundDao = _context.CreateProxy<RefundDao>(refundDao);

        var entityEntry = await _context.AddAsync(refundDao);
        await _context.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task<IEnumerable<RefundDao>> GetAllAsync()
    {
        return await _context.Refunds.ToListAsync();
    }

    public async Task<IEnumerable<RefundDao>> GetAllAsyncByBusinessId(Guid businessId)
    {
        return await _context.Refunds.Where(x => x.Order.BusinessId == businessId).ToListAsync();
    }

    public async Task<RefundDao> GetAsync(Guid id)
    {
        return await _context.Refunds.FindAsync(id) ?? throw new LoomsNotFoundException("Refund not found");
    }

    public async Task<RefundDao> GetAsyncByIdAndBusinessId(Guid id, Guid businessId)
    {
        return await _context.Refunds.FirstOrDefaultAsync(x => x.Id == id && x.Order.BusinessId == businessId)
            ?? throw new LoomsNotFoundException("Refund not found");
    }

    public async Task<RefundDao> GetAsyncByExternalId(string externalId)
    {
        return await _context.Refunds.FirstOrDefaultAsync(x => x.ExternalId == externalId)
            ?? throw new LoomsNotFoundException("Refund not found");
    }

    public async Task<RefundDao> UpdateAsync(RefundDao refundDao)
    {
        await RemoveAsync(refundDao.Id);
        _context.Refunds.Update(refundDao);
        await _context.SaveChangesAsync();

        return refundDao;
    }

    private async Task RemoveAsync(Guid id)
    {
        var refundDao = await _context.Refunds.FindAsync(id);
        _context.Refunds.Remove(refundDao!);
    }
}
