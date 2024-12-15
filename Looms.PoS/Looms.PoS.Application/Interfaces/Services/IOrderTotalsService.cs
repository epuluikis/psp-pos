using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.Services;

public interface IOrderTotalsService
{
    decimal CalculateOrderTotal(OrderDao order);
    decimal CalculateOrderTax(OrderDao order);
}