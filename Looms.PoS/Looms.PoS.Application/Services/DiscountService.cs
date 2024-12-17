using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Services;

public class DiscountService : IDiscountService
{
    public decimal CalculateTotalWithDiscount(DiscountDao discount, decimal total)
    {
        if (discount.DiscountType == DiscountType.Percentage)
        {
            return Math.Round(total * decimal.Divide(100 - discount.Value, 100), 2, MidpointRounding.AwayFromZero);
        }

        if (total < discount.Value)
        {
            return 0;
        }

        return total - discount.Value;
    }
}
