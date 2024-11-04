using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Interfaces;
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

    public IEnumerable<DiscountDao> GetAll()
    {
        return _context.Discounts;
    }

    public Task<DiscountDao> GetAsync(string id)
    {
        throw new NotImplementedException();
    }

    public void DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    Task<BusinessDao> IDiscountsRepository.CreateAsync(DiscountDao discountDao)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<DiscountDao>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}