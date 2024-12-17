using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Interfaces.Services;
using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Interfaces;

namespace Looms.PoS.Application.Services;

public class ProductVariationService : IProductVariationService
{
    private readonly IProductVariationRepository _variationsRepository;
    private readonly IProductVariationModelsResolver _productVariationModelsResolver;

    public ProductVariationService(
        IProductVariationRepository variationsRepository,
        IProductVariationModelsResolver productVariationModelsResolver)
    {
        _variationsRepository = variationsRepository;
        _productVariationModelsResolver = productVariationModelsResolver;
    }

    public async Task<ProductVariationDao> UpdateStock(ProductVariationDao productVariationDao, int quantity)
    {
        var updatedVariationDao = _productVariationModelsResolver.GetUpdatedQuantityDao(productVariationDao, quantity);

        return await _variationsRepository.UpdateAsync(updatedVariationDao);
    }
}
