using AutoMapper;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.ProductVariation;
using Looms.PoS.Application.Models.Responses.ProductVariation;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.ModelsResolvers;

public class ProductVariationModelsResolver : IProductVariationModelsResolver
{
    private readonly IMapper _mapper;

    public ProductVariationModelsResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public ProductVariationDao GetDaoFromRequest(CreateProductVariationRequest createProductVariationRequest)
    {
        return _mapper.Map<ProductVariationDao>(createProductVariationRequest);
    }

    public ProductVariationResponse GetResponseFromDao(ProductVariationDao productVariationDao)
    {
        return _mapper.Map<ProductVariationResponse>(productVariationDao);
    }

    public IEnumerable<ProductVariationResponse> GetResponseFromDao(IEnumerable<ProductVariationDao> productVariationDao)
    {
        return _mapper.Map<IEnumerable<ProductVariationResponse>>(productVariationDao);
    }

    ProductVariationDao IProductVariationModelsResolver.GetDaoFromDaoAndRequest(ProductVariationDao originalDao, UpdateProductVariationRequest updateProductVariationRequest)
    {
        return _mapper.Map<ProductVariationDao>(updateProductVariationRequest) with
        {
            Id = originalDao.Id,
        };
    }

    ProductVariationDao IProductVariationModelsResolver.GetDeletedDao(ProductVariationDao originalDao)
    {
        return originalDao with
        {
            IsDeleted = true
        };
    }

    public ProductVariationDao GetUpdatedQuantityDao(ProductVariationDao originalDao, int quantity)
    {
        return originalDao with
        {
            Quantity = originalDao.Quantity - quantity
        };
    }
}
