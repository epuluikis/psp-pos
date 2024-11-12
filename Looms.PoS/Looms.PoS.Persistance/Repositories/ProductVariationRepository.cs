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

    public async Task<ProductVariationDao> CreateAsync(ProductVariationDao paymentDao)
    {
        var entityEntry = await _context.AddAsync(paymentDao);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<IEnumerable<ProductVariationDao>> GetAllAsync()
    {
        return await _context.ProductVariations.ToListAsync();
    }

    public async Task<ProductVariationDao> GetAsync(Guid id)
    {
        return await _context.ProductVariations.FindAsync(id)
            ?? throw new LoomsNotFoundException("Product Variation entry not found");
    }
}
