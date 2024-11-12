using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Persistance.Repositories;

public class GiftCardsRepository : IGiftCardsRepository
{
    private readonly AppDbContext _context;

    public GiftCardsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<GiftCardDao> CreateAsync(GiftCardDao giftCardDao)
    {
        var entityEntry = await _context.AddAsync(giftCardDao);
        await Save();
        return entityEntry.Entity;
    }

    public async Task<IEnumerable<GiftCardDao>> GetAllAsync()
    {
        return await _context.GiftCards.ToListAsync();
    }

    public async Task<GiftCardDao> GetAsync(Guid id)
    {
        return await _context.GiftCards.FindAsync(id)
            ?? throw new LoomsNotFoundException("GiftCard not found");
    }

    public void DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }
}
