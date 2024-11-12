using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IProductStockRepository
{
    Task<ProductStockDao> CreateAsync(ProductStockDao productStockDao);
    Task<IEnumerable<ProductStockDao>> GetAllAsync();
    Task<ProductStockDao> GetAsync(Guid id);
    Task<ProductStockDao> UpdateAsync(ProductStockDao productStockDao);
}
