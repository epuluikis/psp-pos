using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IDiscountsRepository
{
    Task<DiscountDao> CreateAsync(DiscountDao discountDao);
    Task<IEnumerable<DiscountDao>> GetAllAsync();
    Task<IEnumerable<DiscountDao>> GetAllAsyncByBusinessId(Guid businessId);
    Task<DiscountDao> GetAsync(Guid id);
    Task<DiscountDao> GetAsyncByIdAndBusinessId(Guid id, Guid businessId);
    Task<DiscountDao> UpdateAsync(DiscountDao discountDao);
}
