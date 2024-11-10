using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Persistance.Repositories;

public class PaymentsRepository : IPaymentsRepository
{
    private readonly AppDbContext _context;

    public PaymentsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PaymentDao> CreateAsync(PaymentDao paymentDao)
    {
        var entityEntry = await _context.AddAsync(paymentDao);
        await _context.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<IEnumerable<PaymentDao>> GetAllAsync()
    {
        return await _context.Payments.ToListAsync();
    }

    public async Task<PaymentDao> GetAsync(Guid id)
    {
        return await _context.Payments.FindAsync(id)
            ?? throw new LoomsNotFoundException("Payment not found");
    }

    public void DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
