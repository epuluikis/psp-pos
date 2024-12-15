using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.Services;

public interface IProductUpdatesService
{
    Task<ProductDao> UpdateProductStock(ProductDao productDao, int quantity);
}