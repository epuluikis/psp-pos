using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Exceptions;
using Looms.PoS.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Looms.PoS.Domain.Filters.Order;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Persistence.Repositories;

public class OrdersRepository : LoomsException, IOrdersRepository
{
    private readonly AppDbContext _context;

    public OrdersRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<OrderDao> CreateAsync(OrderDao orderDao)
    {
        orderDao = _context.CreateProxy<OrderDao>(orderDao);

        var entityEntry = await _context.Orders.AddAsync(orderDao);
        await _context.SaveChangesAsync();

        return entityEntry.Entity;
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
                             .ThenInclude(oi => oi.Service)
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
        var query = _context.Orders.AsQueryable();

        if (filter.Status is not null)
        {
            query = query.Where(x => x.Status == Enum.Parse<OrderStatus>(filter.Status));
        }

        if (filter.UserId is not null)
        {
            query = query.Where(x => x.UserId == Guid.Parse(filter.UserId));
        }

        return await query.Where(x => !x.IsDeleted)
                          .Include(x => x.Discount)
                          .Include(x => x.Payments)
                          .Include(x => x.Refunds)
                          .Include(x => x.Business)
                          .Include(x => x.OrderItems.Where(oi => !oi.IsDeleted))
                          .ThenInclude(oi => oi.Service)
                          .Include(x => x.OrderItems.Where(oi => !oi.IsDeleted))
                          .ThenInclude(oi => oi.ProductVariation)
                          .Include(x => x.OrderItems.Where(oi => !oi.IsDeleted))
                          .ThenInclude(oi => oi.Discount)
                          .Include(x => x.OrderItems.Where(oi => !oi.IsDeleted))
                          .ThenInclude(oi => oi.Product)
                          .ThenInclude(p => p.Tax)
                          .ToListAsync();
    }

    public async Task<IEnumerable<OrderDao>> GetAllAsyncByBusinessId(GetAllOrdersFilter filter, Guid businessId)
    {
        var query = _context.Orders.AsQueryable();

        if (filter.Status is not null)
        {
            query = query.Where(x => x.Status == Enum.Parse<OrderStatus>(filter.Status));
        }

        if (filter.UserId is not null)
        {
            query = query.Where(x => x.UserId == Guid.Parse(filter.UserId));
        }

        return await query.Where(x => !x.IsDeleted && x.BusinessId == businessId).ToListAsync();
    }


    public async Task<OrderDao> GetAsync(Guid id)
    {
        var orderDao = await _context.Orders
                                     .Include(x => x.Discount)
                                     .Include(x => x.Payments)
                                     .Include(x => x.Refunds)
                                     .Include(x => x.Business)
                                     .Include(x => x.OrderItems)
                                     .Where(x => x.IsDeleted == false)
                                     .Include(x => x.OrderItems)
                                     .ThenInclude(oi => oi.Service)
                                     .Include(x => x.OrderItems)
                                     .ThenInclude(oi => oi.ProductVariation)
                                     .Include(x => x.OrderItems)
                                     .ThenInclude(oi => oi.Discount)
                                     .Include(x => x.OrderItems)
                                     .ThenInclude(oi => oi.Product)
                                     .ThenInclude(p => p.Tax)
                                     .FirstOrDefaultAsync(x => x.Id == id);


        if (orderDao is null || orderDao.IsDeleted)
        {
            throw new LoomsNotFoundException("Order not found");
        }

        return orderDao;
    }

    public async Task<OrderDao> GetAsyncByIdAndBusinessId(Guid id, Guid businessId)
    {
        var orderDao = await _context.Orders.Where(x => x.Id == id && x.BusinessId == businessId).FirstOrDefaultAsync();

        if (orderDao is null || orderDao.IsDeleted)
        {
            throw new LoomsNotFoundException("Order not found");
        }

        return orderDao;
    }

    public async Task<OrderDao> UpdateAsync(OrderDao orderDao)
    {
        var existingOrderDao = await _context.Orders.FindAsync(orderDao.Id);

        if (existingOrderDao is null || existingOrderDao.IsDeleted)
        {
            throw new LoomsNotFoundException("Order not found");
        }

        _context.Entry(existingOrderDao).CurrentValues.SetValues(orderDao);
        await _context.SaveChangesAsync();

        return existingOrderDao;
    }
}
