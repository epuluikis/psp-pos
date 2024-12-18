using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Persistence.Repositories;

public class OrderItemsRepository : LoomsException, IOrderItemsRepository
{
    private readonly AppDbContext _context;

    public OrderItemsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OrderItemDao> CreateAsync(OrderItemDao orderItemDao)
    {
        orderItemDao = _context.CreateProxy<OrderItemDao>(orderItemDao);

        var entityEntry = await _context.OrderItems.AddAsync(orderItemDao);
        await _context.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task<IEnumerable<OrderItemDao>> GetAllAsync(Guid orderId)
    {
        return await _context.OrderItems.Where(x => x.OrderId == orderId && x.IsDeleted == false).ToListAsync();
    }

    public async Task<OrderItemDao> GetAsync(Guid id)
    {
        var orderItemDao = await _context.OrderItems
                                         .Include(x => x.ProductVariation)
                                         .Include(x => x.Discount)
                                         .Include(x => x.Service)
                                         .Include(x => x.Product)
                                         .ThenInclude(p => p.Tax)
                                         .FirstOrDefaultAsync(x => x.Id == id);

        if (orderItemDao is null || orderItemDao.IsDeleted)
        {
            throw new LoomsNotFoundException("Order item not found");
        }

        return orderItemDao;
    }

    public async Task<OrderItemDao> GetAsyncByIdAndOrderIdAndBusinessId(Guid id, Guid orderId, Guid businessId)
    {
        var orderItemDao = await _context.OrderItems.Where(x => x.Id == id && x.OrderId == orderId && x.Order.BusinessId == businessId)
                                         .FirstOrDefaultAsync();

        if (orderItemDao is null || orderItemDao.IsDeleted)
        {
            throw new LoomsNotFoundException("Order item not found");
        }

        return orderItemDao;
    }

    public async Task<OrderItemDao> UpdateAsync(OrderItemDao orderItem)
    {
        await RemoveAsync(orderItem.Id);
        _context.OrderItems.Update(orderItem);
        await _context.SaveChangesAsync();

        return orderItem;
    }

    public async Task RemoveAsync(Guid id)
    {
        var orderItemDao = await _context.OrderItems.FindAsync(id);
        _context.OrderItems.Remove(orderItemDao!);
    }
}
