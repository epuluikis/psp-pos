using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IDiscountsRepository
{
    Task<DiscountDao> CreateAsync(DiscountDao discountDao);
    Task<IEnumerable<DiscountDao>> GetAllAsync();
    Task<DiscountDao> GetAsync(Guid id);
    Task<DiscountDao> UpdateAsync(DiscountDao discountDao);
    Task DeleteAsync(Guid id);
    Task ArchiveDiscountAsync(Guid id);
}
