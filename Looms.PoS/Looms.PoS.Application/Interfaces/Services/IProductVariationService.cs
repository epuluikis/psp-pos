using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.Services;

public interface IProductVariationService
{
    Task<ProductVariationDao> UpdateStock(ProductVariationDao productVariationDao, int quantity);
}
