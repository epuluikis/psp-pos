using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IProductVariationRepository
{
    Task<ProductVariationDao> CreateAsync(ProductVariationDao productVariationDao);
    Task<IEnumerable<ProductVariationDao>> GetAllAsync();
    Task<IEnumerable<ProductVariationDao>> GetAllAsync(Guid productId);
    Task<ProductVariationDao> GetAsync(Guid id);
    Task<ProductVariationDao> GetAsyncByIdAndProductId(Guid id, Guid productId);
    Task<ProductVariationDao> UpdateAsync(ProductVariationDao productVariationDao);
}
