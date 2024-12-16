using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Looms.PoS.Domain.Filters.Order;
using Looms.PoS.Domain.Enums;

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
        return await _context.Orders
            .Where(x => !x.IsDeleted)
            .Include(x => x.Discount)
            .Include(x => x.Payments)
            .Include(x => x.Refunds)
            .Include(x => x.Business)
            .Include(x => x.OrderItems.Where(oi => !oi.IsDeleted))
                .ThenInclude(oi => oi.Reservation)
                    .ThenInclude(r => r.Service)
                        .ThenInclude(s => s.Tax)
            .Include(x => x.OrderItems.Where(oi => !oi.IsDeleted))
                .ThenInclude(oi => oi.ProductVariation)
            .Include(x => x.OrderItems.Where(oi => !oi.IsDeleted))
                .ThenInclude(oi => oi.Discount)
            .Include(x => x.OrderItems.Where(oi => !oi.IsDeleted))
                .ThenInclude(oi => oi.Product)
                    .ThenInclude(p => p.Tax)
            .ToListAsync();
    }

    public async Task<IEnumerable<OrderDao>> GetAllAsync(GetAllOrdersFilter filter)
    {
        var query = _context.Orders
            .Where(x => !x.IsDeleted);

        if(filter.Status is not null){
            query = query.Where(x => x.Status == Enum.Parse<OrderStatus>(filter.Status));
        }
        if(filter.UserId is not null){
            query = query.Where(x => x.UserId == Guid.Parse(filter.UserId));
        }

        query = query.Include(x => x.Discount)
            .Include(x => x.Payments)
            .Include(x => x.Refunds)
            .Include(x => x.Business)
            .Include(x => x.OrderItems.Where(oi => !oi.IsDeleted))
                .ThenInclude(oi => oi.Reservation)
                    .ThenInclude(r => r.Service)
                        .ThenInclude(s => s.Tax)
            .Include(x => x.OrderItems.Where(oi => !oi.IsDeleted))
                .ThenInclude(oi => oi.ProductVariation)
            .Include(x => x.OrderItems.Where(oi => !oi.IsDeleted))
                .ThenInclude(oi => oi.Discount)
            .Include(x => x.OrderItems.Where(oi => !oi.IsDeleted))
                .ThenInclude(oi => oi.Product)
                    .ThenInclude(p => p.Tax);

        return await query.ToListAsync();
    }


    public async Task<OrderDao> GetAsync(Guid id)
    {
    var order = await _context.Orders
        .Include(x => x.Discount)
        .Include(x => x.Payments)
        .Include(x => x.Refunds)
        .Include(x => x.Business)
        .Include(x => x.OrderItems)
            .Where(x => x.IsDeleted == false)
        .Include(x => x.OrderItems.Where(oi => !oi.IsDeleted))
            .ThenInclude(oi => oi.Reservation)
                .ThenInclude(r => r.Service)
                    .ThenInclude(s => s.Tax)
        .Include(x => x.OrderItems.Where(x => x.IsDeleted == false))
            .ThenInclude(oi => oi.ProductVariation)
        .Include(x => x.OrderItems.Where(x => x.IsDeleted == false))
            .ThenInclude(oi => oi.Discount)
        .Include(x => x.OrderItems.Where(x => x.IsDeleted == false))
            .ThenInclude(oi => oi.Product)
                .ThenInclude(p => p.Tax)
        .FirstOrDefaultAsync(x => x.Id == id);

        ValidateOrder(order);

        return order;
    }

    public async Task<OrderDao> UpdateAsync(OrderDao order)
    {
        var existingOrder = await _context.Orders.FindAsync(order.Id);

        ValidateOrder(existingOrder);

        _context.Entry(existingOrder).CurrentValues.SetValues(order);

        await _context.SaveChangesAsync();
        return existingOrder;
    }

    private void ValidateOrder(OrderDao order)
    {
        if(order is null || order.IsDeleted)
        {
            throw new LoomsNotFoundException("Order not found");
        }
    }
}