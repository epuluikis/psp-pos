using Looms.PoS.Application.Models.Requests.Product;
using Looms.PoS.Application.Models.Responses.Product;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;

public interface IProductModelsResolver
{
    ProductDao GetDaoFromRequest(CreateProductRequest createProductRequest);
    ProductDao GetDaoFromDaoAndRequest(ProductDao originalDao, UpdateProductRequest updateProductRequest);
    ProductDao GetDeletedDao(ProductDao originalDao);
    ProductResponse GetResponseFromDao(ProductDao productDao);
    IEnumerable<ProductResponse> GetResponseFromDao(IEnumerable<ProductDao> productDao);
}
