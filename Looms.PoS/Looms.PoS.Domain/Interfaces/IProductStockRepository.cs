using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IProductStockRepository
{
    Task<ProductStockDao> CreateAsync(ProductStockDao paymentDao);
    Task<IEnumerable<ProductStockDao>> GetAllAsync();
    Task<ProductStockDao> GetAsync(Guid id);
}
