using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using System.Data.Entity;

namespace Looms.PoS.Persistance.Repositories;

public class OrdersRepository : LoomsException, IOrdersRepository
{
    private readonly AppDbContext _context;

    public OrdersRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OrderDao> CreateAsync(OrderDao order)
    {
        var orderDao = await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        return orderDao.Entity;
    }

    public async Task RemoveAsync(Guid id)
    {
        var order = await _context.Orders.FindAsync(id);
        _context.Orders.Remove(order!);
    }

    public async Task<IEnumerable<OrderDao>> GetAllAsync()
    {
        return await _context.Orders.Where(x => x.IsDeleted == false).ToListAsync();
    }

    public async Task<OrderDao> GetAsync(Guid id)
    {
        var order = await _context.Orders.FindAsync(id);

        ValidateOrder(order);

        await _context.Entry(order)
            .Collection(o => o.OrderItems)
            .LoadAsync();

        return order;
    }

    public async Task<OrderDao> UpdateAsync(OrderDao order)
    {
        ValidateOrder(order);

        await RemoveAsync(order.Id);
        _context.Orders.Update(order);
        _context.SaveChanges();

        return order;
    }

    private void ValidateOrder(OrderDao order)
    {
        if(order is null || order.IsDeleted)
        {
            throw new LoomsNotFoundException("Order not found");
        }
    }
}