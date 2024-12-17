using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.Services;

public interface IProductService
{
    Task<ProductDao> UpdateStock(ProductDao productDao, int quantity);
}
