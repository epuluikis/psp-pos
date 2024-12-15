using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IProductVariationRepository
{
    Task<ProductVariationDao> CreateAsync(ProductVariationDao productVariationDao);
    Task<IEnumerable<ProductVariationDao>> GetAllAsync();
    Task<ProductVariationDao> GetAsync(Guid id);
    Task<ProductVariationDao> UpdateAsync(ProductVariationDao productVariationDao);
}
