using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IProductsRepository
{
    Task<ProductDao> CreateAsync(ProductDao productDao);
    Task<IEnumerable<ProductDao>> GetAllAsync();
    Task<ProductDao> GetAsync(Guid id);
    Task<ProductDao> UpdateAsync(ProductDao productDao);
}
