using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Application.Services;

public class DiscountsTotalsService : IDiscountTotalsService
{
    public decimal CalculateTotalWithDiscount(DiscountDao discount, decimal total)
    {
        if(discount.DiscountType == DiscountType.Percentage)
        {
            total = Math.Round(total * Decimal.Divide((100 - discount.Value), 100), 2, MidpointRounding.AwayFromZero);
        }
        else
        {
            if(total < discount.Value)
            {
                return 0;
            }

            total -= discount.Value;
        }

        return total;
    }

}