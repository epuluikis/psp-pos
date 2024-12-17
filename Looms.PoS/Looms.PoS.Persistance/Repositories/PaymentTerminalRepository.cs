using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Persistance.Repositories;

public class PaymentTerminalsRepository : IPaymentTerminalsRepository
{
    private readonly AppDbContext _context;

    public PaymentTerminalsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PaymentTerminalDao> CreateAsync(PaymentTerminalDao paymentTerminalDao)
    {
        paymentTerminalDao = _context.CreateProxy<PaymentTerminalDao>(paymentTerminalDao);

        var entityEntry = await _context.AddAsync(paymentTerminalDao);
        await _context.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task<IEnumerable<PaymentTerminalDao>> GetAllAsync()
    {
        return await _context.PaymentTerminals.Where(x => !x.IsDeleted).ToListAsync();
    }

    public async Task<PaymentTerminalDao> GetAsync(Guid id)
    {
        var paymentTerminalDao = await _context.PaymentTerminals.FindAsync(id);

        if (paymentTerminalDao is null || paymentTerminalDao.IsDeleted)
        {
            throw new LoomsNotFoundException("PaymentTerminal not found");
        }

        return paymentTerminalDao;
    }

    public async Task<PaymentTerminalDao> UpdateAsync(PaymentTerminalDao paymentTerminalDao)
    {
        await RemoveAsync(paymentTerminalDao.Id);
        _context.PaymentTerminals.Update(paymentTerminalDao);
        await _context.SaveChangesAsync();

        return paymentTerminalDao;
    }

    private async Task RemoveAsync(Guid id)
    {
        var paymentTerminalDao = await _context.PaymentTerminals.FindAsync(id);
        _context.PaymentTerminals.Remove(paymentTerminalDao!);
    }
}
