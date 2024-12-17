using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductsRepository _productRepository;
    private readonly IProductModelsResolver _productModelsResolver;

    public ProductService(
        IProductsRepository productRepository,
        IProductModelsResolver productModelsResolver)
    {
        _productRepository = productRepository;
        _productModelsResolver = productModelsResolver;
    }

    public async Task<ProductDao> UpdateStock(ProductDao productDao, int quantity)
    {
        var updatedProductDao = _productModelsResolver.GetUpdatedQuantityDao(productDao, quantity);
        var updatedDao = await _productRepository.UpdateAsync(updatedProductDao);

        return updatedDao;
    }
}
