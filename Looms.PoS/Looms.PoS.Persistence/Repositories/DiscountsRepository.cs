using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Persistence.Repositories;

// TODO: Add Get Active discounts for orders and order items

public class DiscountsRepository : IDiscountsRepository
{
    private readonly AppDbContext _context;

    public DiscountsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DiscountDao> CreateAsync(DiscountDao discountDao)
    {
        discountDao = _context.CreateProxy<DiscountDao>(discountDao);

        var entityEntry = await _context.AddAsync(discountDao);
        await _context.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task<IEnumerable<DiscountDao>> GetAllAsync()
    {
        return await _context.Discounts.Where(x => !x.IsDeleted).ToListAsync();
    }

    public async Task<IEnumerable<DiscountDao>> GetAllAsyncByBusinessId(Guid businessId)
    {
        return await _context.Discounts.Where(x => !x.IsDeleted && x.BusinessId == businessId).ToListAsync();
    }

    public async Task<DiscountDao> GetAsync(Guid id)
    {
        var discountDao = await _context.Discounts.FindAsync(id);

        if (discountDao is null || discountDao.IsDeleted)
        {
            throw new LoomsNotFoundException("Discount not found");
        }

        return discountDao;
    }

    public async Task<DiscountDao> GetAsyncByIdAndBusinessId(Guid id, Guid businessId)
    {
        var discountDao = await _context.Discounts.Where(x => x.Id == id && x.BusinessId == businessId).FirstOrDefaultAsync();

        if (discountDao is null || discountDao.IsDeleted)
        {
            throw new LoomsNotFoundException("Discount not found");
        }

        return discountDao;
    }

    public async Task<DiscountDao> UpdateAsync(DiscountDao discountDao)
    {
        await RemoveAsync(discountDao.Id);
        _context.Discounts.Update(discountDao);
        await _context.SaveChangesAsync();

        return discountDao;
    }

    private async Task RemoveAsync(Guid id)
    {
        var discount = await _context.Discounts.FindAsync(id);
        _context.Discounts.Remove(discount!);
    }
}
