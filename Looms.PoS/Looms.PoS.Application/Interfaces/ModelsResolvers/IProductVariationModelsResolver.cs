using Looms.PoS.Application.Models.Requests.ProductVariation;
using Looms.PoS.Application.Models.Responses.Product;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;

public interface IProductVariationModelsResolver
{
    ProductVariationDao GetDaoFromRequest(CreateProductVariationRequest createProductVariationRequest);
    ProductVariationDao GetDaoFromDaoAndRequest(ProductVariationDao originalDao, UpdateProductVariationRequest updateProductVariationRequest);
    ProductVariationDao GetDaoFromDaoAndRequest(ProductVariationDao originalDao, UpdateProductVariationRequest updateProductVariationRequest, ProductDao productDao);
    ProductVariationDao GetDeletedDao(ProductVariationDao originalDao);
    ProductVariationResponse GetResponseFromDao(ProductVariationDao productDao);
    IEnumerable<ProductVariationResponse> GetResponseFromDao(IEnumerable<ProductVariationDao> productVariationDao);
    ProductVariationDao GetUpdatedQuantityDao(ProductVariationDao originalDao, int quantity);

}
