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
        giftCardDao = _context.CreateProxy<GiftCardDao>(giftCardDao);

        var entityEntry = await _context.AddAsync(giftCardDao);
        await _context.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task<IEnumerable<GiftCardDao>> GetAllAsync()
    {
        return await _context.GiftCards.Where(x => !x.IsDeleted).ToListAsync();
    }

    public async Task<GiftCardDao> GetAsync(Guid id)
    {
        var giftCardDao = await _context.GiftCards.FindAsync(id);

        if (giftCardDao is null || giftCardDao.IsDeleted)
        {
            throw new LoomsNotFoundException("GiftCard not found");
        }

        return giftCardDao;
    }

    public async Task<GiftCardDao> GetAsyncByBusinessIdAndCode(Guid businessId, string code)
    {
        var giftCardDao = await _context.GiftCards.Where(x => x.BusinessId == businessId && x.Code == code).FirstAsync();

        if (giftCardDao is null || giftCardDao.IsDeleted)
        {
            throw new LoomsNotFoundException("GiftCard not found");
        }

        return giftCardDao;
    }

    public async Task<GiftCardDao> UpdateAsync(GiftCardDao giftCardDao)
    {
        await RemoveAsync(giftCardDao.Id);
        _context.GiftCards.Update(giftCardDao);
        await _context.SaveChangesAsync();

        return giftCardDao;
    }

    private async Task RemoveAsync(Guid id)
    {
        var giftCardDao = await _context.GiftCards.FindAsync(id);
        _context.GiftCards.Remove(giftCardDao!);
    }
}
