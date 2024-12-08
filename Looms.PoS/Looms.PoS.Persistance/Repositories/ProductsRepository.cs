using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Persistance.Repositories;

public class ProductsRepository : IProductsRepository
{
    private readonly AppDbContext _context;

    public ProductsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ProductDao> CreateAsync(ProductDao productDao)
    {
        var entityEntry = await _context.AddAsync(productDao);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<IEnumerable<ProductDao>> GetAllAsync()
    {
        return await _context.Products.Where(x => !x.IsDeleted).ToListAsync();
    }

    public async Task<ProductDao> GetAsync(Guid id)
    {
        var productDao = await _context.Products.FindAsync(id);

        if (productDao is null || productDao.IsDeleted)
        {
            throw new LoomsNotFoundException("Product not found");
        }
        return productDao;
    }

    public async Task<ProductDao> UpdateAsync(ProductDao productDao)
    {
        await RemoveAsync(productDao.Id);
        _context.Products.Update(productDao);
        await _context.SaveChangesAsync();
        return productDao;
    }

    private async Task RemoveAsync(Guid id)
    {
        var productDao = await _context.Products.FindAsync(id);
        _context.Products.Remove(productDao!);
    }
}
