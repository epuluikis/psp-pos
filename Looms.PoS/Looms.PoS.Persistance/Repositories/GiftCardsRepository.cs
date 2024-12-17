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

    public async Task<IEnumerable<GiftCardDao>> GetAllAsyncByBusinessId(Guid businessId)
    {
        return await _context.GiftCards.Where(x => !x.IsDeleted && x.BusinessId == businessId).ToListAsync();
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

    public async Task<GiftCardDao> GetAsyncByIdAndBusinessId(Guid id, Guid businessId)
    {
        var giftCardDao = await _context.GiftCards.Where(x => x.Id == id && x.BusinessId == businessId).FirstOrDefaultAsync();

        if (giftCardDao is null || giftCardDao.IsDeleted)
        {
            throw new LoomsNotFoundException("GiftCard not found");
        }

        return giftCardDao;
    }

    public async Task<GiftCardDao> GetAsyncByBusinessIdAndCode(Guid businessId, string code)
    {
        var giftCardDao = await _context.GiftCards.Where(x => x.BusinessId == businessId && x.Code == code).FirstOrDefaultAsync();

        if (giftCardDao is null || giftCardDao.IsDeleted)
        {
            throw new LoomsNotFoundException("GiftCard not found");
        }

        return giftCardDao;
    }

    public async Task<bool> ExistsAsyncWithCodeAndBusinessId(string code, Guid businessId)
    {
        return await _context.GiftCards.AnyAsync(x => !x.IsDeleted && x.BusinessId == businessId && x.Code == code);
    }

    public async Task<bool> ExistsAsyncWithCodeAndBusinessIdExcludingId(string code, Guid businessId, Guid excludeId)
    {
        return await _context.GiftCards.AnyAsync(x => !x.IsDeleted && x.BusinessId == businessId && x.Code == code && x.Id != excludeId);
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
