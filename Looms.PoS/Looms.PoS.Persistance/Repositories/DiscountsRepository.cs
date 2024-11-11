using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Looms.PoS.Persistance.Repositories;

public class DiscountsRepository : IDiscountsRepository
{
    private readonly AppDbContext _context;

    public DiscountsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DiscountDao> CreateAsync(DiscountDao discountDao)
    {
        var entityEntry = await _context.AddAsync(discountDao);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<DiscountDao> GetAsync(Guid id)
    {
        return await _context.Discounts.FindAsync(id)
            ?? throw new LoomsNotFoundException("Discount not found");
    }

    public async Task<IEnumerable<DiscountDao>> GetAllAsync()
    {
        return await _context.Discounts.Where(x => !x.IsDeleted).ToListAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var discount = await _context.Discounts.FindAsync(id)
            ?? throw new LoomsNotFoundException("No valid discount provided");
        _context.Discounts.Remove(discount);
        await _context.SaveChangesAsync();
    }

    public async Task ArchiveDiscountAsync(Guid id)
    {
        var discount = await _context.Discounts.FindAsync(id)
            ?? throw new LoomsNotFoundException("No valid discount provided");
        discount.IsDeleted = true;
        await _context.SaveChangesAsync();
    }

    public async Task<DiscountDao> UpdateAsync(DiscountDao discountDao)
    {
        var discount = await _context.Discounts.FindAsync(discountDao.Id)
            ?? throw new LoomsNotFoundException("No valid discount provided");

        discount.Name = discountDao.Name;
        discount.DiscountType = discountDao.DiscountType;
        discount.Value = discountDao.Value;
        discount.Target = discountDao.Target;
        discount.ProductId = discountDao.ProductId;
        discount.StartDate = discountDao.StartDate;
        discount.EndDate = discountDao.EndDate;

        await _context.SaveChangesAsync();
        return discount;
    }
}