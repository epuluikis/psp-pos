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

    public async Task<ProductDao> CreateAsync(ProductDao paymentDao)
    {
        var entityEntry = await _context.AddAsync(paymentDao);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<IEnumerable<ProductDao>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<ProductDao> GetAsync(Guid id)
    {
        return await _context.Products.FindAsync(id)
            ?? throw new LoomsNotFoundException("Product not found");
    }
}
