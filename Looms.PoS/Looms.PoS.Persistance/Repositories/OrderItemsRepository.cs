using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Looms.PoS.Persistance.Repositories;

public class OrderItemsRepository : LoomsException, IOrderItemsRepository
{
    private readonly AppDbContext _context;

    public OrderItemsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OrderItemDao> CreateAsync(OrderItemDao orderItem)
    {
        var orderItemEntity = await _context.OrderItems.AddAsync(orderItem);
        await _context.SaveChangesAsync();
        return orderItemEntity.Entity;
    }

    public async Task RemoveAsync(Guid id)
    {
        var orderItem = await _context.OrderItems.FindAsync(id);
        _context.OrderItems.Remove(orderItem!);
    }

    public async Task<IEnumerable<OrderItemDao>> GetAllAsync(Guid orderId)
    {
        var orderItems = await _context.OrderItems.Where(x => x.OrderId == orderId && x.IsDeleted == false).ToListAsync();
        return orderItems;
    }

    public async Task<OrderItemDao> GetAsync(Guid id)
    {
        var orderItem = await _context.OrderItems
            .Include(x => x.ProductVariation)
            .Include(x => x.Discount)
            .Include(x => x.Service)
            .Include(x => x.Product)
                .ThenInclude(p => p.Tax)
            .FirstOrDefaultAsync(x => x.Id == id);

        ValidateOrderItem(orderItem);

        return orderItem;
    }

    public async Task<OrderItemDao> UpdateAsync(OrderItemDao orderItem)
    {
        ValidateOrderItem(orderItem);

        await RemoveAsync(orderItem.Id);
        _context.OrderItems.Update(orderItem);
        await _context.SaveChangesAsync();

        return orderItem;
    }

    private void ValidateOrderItem(OrderItemDao orderItem)
    {
        if (orderItem is null || orderItem.IsDeleted)
        {
            throw new LoomsNotFoundException("Order item not found");
        }
    }
}
