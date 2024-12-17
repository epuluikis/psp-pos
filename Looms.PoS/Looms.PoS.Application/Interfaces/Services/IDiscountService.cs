using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.Services;

public interface IDiscountService
{
    decimal CalculateTotalWithDiscount(DiscountDao discount, decimal price);
}
