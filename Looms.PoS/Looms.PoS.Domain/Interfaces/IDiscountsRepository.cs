using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;
public interface IDiscountsRepository
{
    Task<DiscountDao> CreateAsync(DiscountDao discountDao);
    Task<IEnumerable<DiscountDao>> GetAllAsync();
    Task<DiscountDao> GetAsync(Guid id);
    Task DeleteAsync(Guid id);
}
