using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IProductsRepository
{
    Task<ProductDao> CreateAsync(ProductDao productDao);
    Task<IEnumerable<ProductDao>> GetAllAsync();
    Task<ProductDao> GetAsync(Guid id);
    Task<IEnumerable<ProductDao>> GetAllByBusinessIdAsync(Guid businessId);
    Task<ProductDao> GetAsyncByIdAndBusinessId(Guid id, Guid businessId);
    Task<ProductDao> UpdateAsync(ProductDao productDao);
}
