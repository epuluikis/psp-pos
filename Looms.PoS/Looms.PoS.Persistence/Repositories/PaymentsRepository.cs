using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Persistence.Repositories;

public class PaymentsRepository : IPaymentsRepository
{
    private readonly AppDbContext _context;

    public PaymentsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PaymentDao> CreateAsync(PaymentDao paymentDao)
    {
        paymentDao = _context.CreateProxy<PaymentDao>(paymentDao);

        var entityEntry = await _context.AddAsync(paymentDao);
        await _context.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task<IEnumerable<PaymentDao>> GetAllAsync()
    {
        return await _context.Payments.Where(x => !x.IsDeleted).ToListAsync();
    }

    public async Task<IEnumerable<PaymentDao>> GetAllAsyncByBusinessId(Guid businessId)
    {
        return await _context.Payments.Where(x => !x.IsDeleted && x.Order.BusinessId == businessId).ToListAsync();
    }

    public async Task<PaymentDao> GetAsync(Guid id)
    {
        var paymentDao = await _context.Payments.FindAsync(id);

        if (paymentDao is null || paymentDao.IsDeleted)
        {
            throw new LoomsNotFoundException("Payment not found");
        }

        return paymentDao;
    }

    public async Task<PaymentDao> GetAsyncByIdAndBusinessId(Guid id, Guid businessId)
    {
        var paymentDao = await _context.Payments.FirstOrDefaultAsync(x => x.Id == id && x.Order.BusinessId == businessId);

        if (paymentDao is null || paymentDao.IsDeleted)
        {
            throw new LoomsNotFoundException("Payment not found");
        }

        return paymentDao;
    }

    public async Task<PaymentDao> GetAsyncByIdAndOrderIdAndBusinessId(Guid id, Guid orderId, Guid businessId)
    {
        var paymentDao = await _context.Payments
                                       .FirstOrDefaultAsync(x => x.Id == id && x.Order.BusinessId == businessId && x.OrderId == orderId);

        if (paymentDao is null || paymentDao.IsDeleted)
        {
            throw new LoomsNotFoundException("Payment not found");
        }

        return paymentDao;
    }

    public async Task<PaymentDao> GetAsyncByExternalId(string externalId)
    {
        var paymentDao = await _context.Payments.Where(x => x.ExternalId == externalId).FirstOrDefaultAsync();

        if (paymentDao is null || paymentDao.IsDeleted)
        {
            throw new LoomsNotFoundException("Payment not found");
        }

        return paymentDao;
    }

    public async Task<PaymentDao> UpdateAsync(PaymentDao paymentDao)
    {
        await RemoveAsync(paymentDao.Id);
        _context.Payments.Update(paymentDao);
        await _context.SaveChangesAsync();

        return paymentDao;
    }

    private async Task RemoveAsync(Guid id)
    {
        var paymentDao = await _context.Payments.FindAsync(id);
        _context.Payments.Remove(paymentDao!);
    }
}
