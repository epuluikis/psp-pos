using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Persistance.Repositories;

public class TaxesRepository : ITaxesRepository
{
    private readonly AppDbContext _context;

    public TaxesRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TaxDao> CreateAsync(TaxDao taxDao)
    {
        taxDao = _context.CreateProxy<TaxDao>(taxDao);

        var entityEntry = await _context.AddAsync(taxDao);
        await _context.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task<IEnumerable<TaxDao>> GetAllAsync()
    {
        return await _context.Taxes.Where(x => !x.IsDeleted).ToListAsync();
    }

    public async Task<IEnumerable<TaxDao>> GetAllAsyncByBusinessId(Guid businessId)
    {
        return await _context.Taxes.Where(x => x.BusinessId == businessId && !x.IsDeleted).ToListAsync();
    }

    public async Task<TaxDao> GetAsync(Guid id)
    {
        var taxDao = await _context.Taxes.FindAsync(id);

        if (taxDao is null || taxDao.IsDeleted)
        {
            throw new LoomsNotFoundException("Tax not found");
        }

        return taxDao;
    }

    public async Task<TaxDao> GetAsyncByIdAndBusinessId(Guid id, Guid businessId)
    {
        var taxDao = await _context.Taxes
            .Where(x => x.BusinessId == businessId && x.Id == id)
            .FirstOrDefaultAsync();

        if (taxDao is null || taxDao.IsDeleted)
        {
            throw new LoomsNotFoundException("Tax not found");
        }

        return taxDao;
    }

    public async Task<TaxDao> GetByTaxCategoryAsync(TaxCategory taxCategory)
    {
        var taxDao = await _context.Taxes.FirstOrDefaultAsync(x => x.TaxCategory == taxCategory);

        taxDao ??= await _context.Taxes.FirstOrDefaultAsync(x => x.TaxCategory == TaxCategory.Both);

        if (taxDao is null || taxDao.IsDeleted)
        {
            throw new LoomsNotFoundException("Tax not found");
        }

        return taxDao;
    }

    public async Task<TaxDao> UpdateAsync(TaxDao taxDao)
    {
        await RemoveAsync(taxDao.Id);
        _context.Taxes.Update(taxDao);
        await _context.SaveChangesAsync();

        return taxDao;
    }

    private async Task RemoveAsync(Guid id)
    {
        var taxDao = await _context.Taxes.FindAsync(id);
        _context.Taxes.Remove(taxDao!);
    }
}
