using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Persistence.Repositories;

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

    public async Task<IEnumerable<PaymentTerminalDao>> GetAllAsyncByBusinessId(Guid businessId)
    {
        return await _context.PaymentTerminals
            .Where(x => !x.IsDeleted && x.PaymentProvider.Business.Id == businessId)
            .ToListAsync();
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

    public async Task<PaymentTerminalDao> GetAsyncByIdAndBusinessId(Guid id, Guid businessId)
    {
        var paymentTerminalDao = await _context.PaymentTerminals
            .Where(x => x.Id == id && x.PaymentProvider.Business.Id == businessId)
            .FirstAsync();
        
        if(paymentTerminalDao is null || paymentTerminalDao.IsDeleted)
        {
            throw new LoomsNotFoundException("PaymentTerminal not found");
        }

        return paymentTerminalDao;
    }

    public async Task<bool> ExistsWithExternalIdAndBusinessId(string externalId, Guid businessId)
    {
        return await _context.PaymentTerminals
            .AnyAsync(x => x.ExternalId == externalId && x.PaymentProvider.Business.Id == businessId);
    }

    public async Task<bool> ExistsWithExternalIdAndBusinessIdExcludingId(string externalId, Guid businessId, Guid excludeId)
    {
        return await _context.PaymentTerminals
            .AnyAsync(x => x.ExternalId == externalId && x.PaymentProvider.Business.Id == businessId && x.Id != excludeId);
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
