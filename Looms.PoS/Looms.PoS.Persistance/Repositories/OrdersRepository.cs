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
        await Save();
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

    public async Task<OrderDao> UpdateDiscountAsync(Guid orderId, Guid discounId)
    {
        var order = await _context.Orders.FindAsync(orderId);
        var discount = await _context.Discounts.FindAsync(discounId);

        ValidateOrder(order);

        _context.Orders.Remove(order!);

        order!.DiscountId = discounId;
        order!.Discount = discount;
        await Save();

        return order;
    }

    public async Task<OrderDao> UpdateItemAsync(Guid orderId, OrderItemDao orderItem)
    {
        var order = await _context.Orders.FindAsync(orderId);

        ValidateOrder(order);

        _context.Orders.Remove(order!);

        order.OrderItems.Add(orderItem);
        await Save();
        return order;
    }

    public async Task<OrderDao> UpdateItemsAsync(Guid orderId, IEnumerable<OrderItemDao> orderItems)
    {
        var order = await _context.Orders.FindAsync(orderId);

        ValidateOrder(order);

        _context.Orders.Remove(order!);
        order.OrderItems.Concat(orderItems);
        await Save();
        return order;
    }

    public async Task<OrderDao> UpdateStatusAsync(Guid orderId, OrderStatus status)
    {
        var order = await _context.Orders.FindAsync(orderId);

        ValidateOrder(order);

        _context.Orders.Remove(order!);

        order!.Status = status;
        await Save();
        return order;
    }

    public Task<OrderDao> UpdateUserAsync(Guid orderId, Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task Save()
    {
        await _context.SaveChangesAsync();
    }

    private void ValidateOrder(OrderDao order)
    {
        if(order is null || order.IsDeleted)
        {
            throw new LoomsNotFoundException("Order not found");
        }
    }
}