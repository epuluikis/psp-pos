using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Services;

public class ProductUpdatesService : IProductUpdatesService
{
    private readonly IProductsRepository _productRepository;
    private readonly IProductModelsResolver _productModelsResolver;

    public ProductUpdatesService(IProductsRepository productRepository,
     IProductModelsResolver productModelsResolver)
    {
        _productRepository = productRepository;
        _productModelsResolver = productModelsResolver;
    }

    public async Task<ProductDao> UpdateProductStock(ProductDao productDao, int quantity)
    {
        var updatedProductDao = _productModelsResolver.GetUpdatedQuantityDao(productDao, quantity);
        var updatedDao = await _productRepository.UpdateAsync(updatedProductDao);
        return updatedDao;
    }

}