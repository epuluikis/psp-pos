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

    public async Task<ProductStockDao> CreateAsync(ProductStockDao paymentDao)
    {
        var entityEntry = await _context.AddAsync(paymentDao);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<IEnumerable<ProductStockDao>> GetAllAsync()
    {
        return await _context.ProductStock.ToListAsync();
    }

    public async Task<ProductStockDao> GetAsync(Guid id)
    {
        return await _context.ProductStock.FindAsync(id)
            ?? throw new LoomsNotFoundException("Product Stock entry not found");
    }
}
