using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Services;

public class ProductVariationUpdatesService : IProductVariationUpdatesService
{
    private readonly IProductVariationRepository _variationsRepository;
    private readonly IProductVariationModelsResolver _productVariationModelsResolver;
    public ProductVariationUpdatesService(IProductVariationRepository variationsRepository, IProductVariationModelsResolver productVariationModelsResolver)
    {
        _variationsRepository = variationsRepository;
        _productVariationModelsResolver = productVariationModelsResolver;
    }

    public async Task<ProductVariationDao> UpdateProductVariationStock(ProductVariationDao productVariationDao, int quantity)
    {
        var updatedVariationDao = _productVariationModelsResolver.GetUpdatedQuantityDao(productVariationDao, quantity);
        var updatedDao = await _variationsRepository.UpdateAsync(updatedVariationDao);
        return updatedDao;
    }
}