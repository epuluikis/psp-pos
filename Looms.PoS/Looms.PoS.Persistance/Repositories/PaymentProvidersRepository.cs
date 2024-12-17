using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Persistance.Repositories;

public class PaymentProvidersRepository : IPaymentProvidersRepository
{
    private readonly AppDbContext _context;

    public PaymentProvidersRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PaymentProviderDao> CreateAsync(PaymentProviderDao paymentProviderDao)
    {
        paymentProviderDao = _context.CreateProxy<PaymentProviderDao>(paymentProviderDao);

        var entityEntry = await _context.AddAsync(paymentProviderDao);
        await _context.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task<IEnumerable<PaymentProviderDao>> GetAllAsync()
    {
        return await _context.PaymentProviders.Where(x => !x.IsDeleted).ToListAsync();
    }

    public async Task<PaymentProviderDao> GetAsync(Guid id)
    {
        var paymentProviderDao = await _context.PaymentProviders.FindAsync(id);

        if (paymentProviderDao is null || paymentProviderDao.IsDeleted)
        {
            throw new LoomsNotFoundException("PaymentProvider not found");
        }

        return paymentProviderDao;
    }

    public async Task<PaymentProviderDao> UpdateAsync(PaymentProviderDao paymentProviderDao)
    {
        await RemoveAsync(paymentProviderDao.Id);
        _context.PaymentProviders.Update(paymentProviderDao);
        await _context.SaveChangesAsync();

        return paymentProviderDao;
    }

    public async Task<bool> ExistsActiveByBusinessId(Guid businessId)
    {
        return await _context.PaymentProviders.AnyAsync(pp => pp.IsActive && pp.BusinessId == businessId);
    }

    public async Task<bool> ExistsActiveByBusinessIdExcludingId(Guid excludeId, Guid businessId)
    {
        return await _context.PaymentProviders.AnyAsync(pp => pp.IsActive && pp.BusinessId != businessId && pp.Id != excludeId);
    }

    private async Task RemoveAsync(Guid id)
    {
        var paymentProviderDao = await _context.PaymentProviders.FindAsync(id);
        _context.PaymentProviders.Remove(paymentProviderDao!);
    }
}
