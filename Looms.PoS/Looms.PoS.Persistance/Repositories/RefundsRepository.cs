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

    public async Task<RefundDao> GetAsync(Guid id)
    {
        return await _context.Refunds.FindAsync(id) ?? throw new LoomsNotFoundException("Refund not found");
    }
}
