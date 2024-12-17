using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Persistance.Repositories;

public class ProductVariationRepository : IProductVariationRepository
{
    private readonly AppDbContext _context;

    public ProductVariationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ProductVariationDao> CreateAsync(ProductVariationDao productVariationDao)
    {
        productVariationDao = _context.CreateProxy<ProductVariationDao>(productVariationDao);

        var entityEntry = await _context.AddAsync(productVariationDao);
        await _context.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task<IEnumerable<ProductVariationDao>> GetAllAsync()
    {
        return await _context.ProductVariations.Where(x => !x.IsDeleted).ToListAsync();
    }

    public async Task<IEnumerable<ProductVariationDao>> GetAllAsync(Guid productId)
    {
        return await _context.ProductVariations.Where(x => !x.IsDeleted && x.ProductId == productId).ToListAsync();
    }

    public async Task<ProductVariationDao> GetAsync(Guid id)
    {
        var productVariationDao = await _context.ProductVariations.FindAsync(id);

        if (productVariationDao is null || productVariationDao.IsDeleted)
        {
            throw new LoomsNotFoundException("Product variation entry not found");
        }

        return productVariationDao;
    }

    public async Task<ProductVariationDao> GetAsyncByIdAndProductId(Guid id, Guid productId)
    {
        var productVariationDao = await _context.ProductVariations.Where(x => x.Id == id && x.ProductId == productId).FirstOrDefaultAsync();

        if (productVariationDao is null || productVariationDao.IsDeleted)
        {
            throw new LoomsNotFoundException("Product variation entry not found");
        }

        return productVariationDao;
    }

    public async Task<ProductVariationDao> UpdateAsync(ProductVariationDao productVariationDao)
    {
        await RemoveAsync(productVariationDao.Id);
        _context.ProductVariations.Update(productVariationDao);
        await _context.SaveChangesAsync();

        return productVariationDao;
    }

    private async Task RemoveAsync(Guid id)
    {
        var productVariationDao = await _context.ProductVariations.FindAsync(id);
        _context.ProductVariations.Remove(productVariationDao!);
    }
}
