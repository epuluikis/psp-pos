using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.Services;

public interface IDiscountTotalsService
{
    decimal CalculateTotalWithDiscount(DiscountDao discount, decimal price);
}