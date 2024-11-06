using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;
public interface IDiscountsRepository
{
    Task<DiscountDao> CreateAsync(DiscountDao discountDao);
    IEnumerable<DiscountDao> GetAll();
    Task<DiscountDao> GetAsync(string id);
    void DeleteAsync(string id);
    Task<DiscountDao> DeleteAsync(Guid id);
}
