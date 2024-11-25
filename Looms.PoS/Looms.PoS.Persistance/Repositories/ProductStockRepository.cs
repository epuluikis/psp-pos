using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Persistance.Repositories;

public class ProductStockRepository : IProductStockRepository
{
    private readonly AppDbContext _context;

    public ProductStockRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ProductStockDao> CreateAsync(ProductStockDao productStockDao)
    {
        var entityEntry = await _context.AddAsync(productStockDao);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<IEnumerable<ProductStockDao>> GetAllAsync()
    {
        return await _context.ProductStock.Where(x => !x.IsDeleted).ToListAsync();
    }

    public async Task<ProductStockDao> GetAsync(Guid id)
    {
        var productStockDao = await _context.ProductStock.FindAsync(id);

        if (productStockDao is null || productStockDao.IsDeleted)
        {
            throw new LoomsNotFoundException("Product stock entry not found");
        }
        return productStockDao;
    }

    public async Task<ProductStockDao> UpdateAsync(ProductStockDao productStockDao)
    {
        await RemoveAsync(productStockDao.Id);
        _context.ProductStock.Update(productStockDao);
        await _context.SaveChangesAsync();
        return productStockDao;
    }

    private async Task RemoveAsync(Guid id)
    {
        var productStockDao = await _context.ProductStock.FindAsync(id);
        _context.ProductStock.Remove(productStockDao!);
    }
}
