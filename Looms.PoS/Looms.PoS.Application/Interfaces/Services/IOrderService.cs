using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.Services;

public interface IOrderService
{
    decimal CalculateTotal(OrderDao order);
    decimal CalculateTax(OrderDao order);
    decimal CalculatePayableAmount(OrderDao order);
    Task SetQuantity(OrderDao orderDao);
    Task ResetQuantity(OrderDao orderDao);
}
