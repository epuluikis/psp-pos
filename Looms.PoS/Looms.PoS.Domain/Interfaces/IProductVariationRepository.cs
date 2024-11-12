using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IProductVariationRepository
{
    Task<ProductVariationDao> CreateAsync(ProductVariationDao paymentDao);
    Task<IEnumerable<ProductVariationDao>> GetAllAsync();
    Task<ProductVariationDao> GetAsync(Guid id);
}
