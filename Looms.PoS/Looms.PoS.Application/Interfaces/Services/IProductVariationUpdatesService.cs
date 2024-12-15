using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.Services;

public interface IProductVariationUpdatesService
{
    Task<ProductVariationDao> UpdateProductVariationStock(ProductVariationDao productVariationDao, int quantity);
}