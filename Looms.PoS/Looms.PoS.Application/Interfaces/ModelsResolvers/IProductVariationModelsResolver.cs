﻿using Looms.PoS.Application.Models.Requests.ProductVariation;
using Looms.PoS.Application.Models.Responses.ProductVariation;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.ModelsResolvers;

public interface IProductVariationModelsResolver
{
    ProductVariationDao GetDaoFromRequest(CreateProductVariationRequest createProductVariationRequest);
    ProductVariationDao GetDaoFromDaoAndRequest(ProductVariationDao originalDao, UpdateProductVariationRequest updateProductVariationRequest);
    ProductVariationDao GetDeletedDao(ProductVariationDao originalDao);
    ProductVariationResponse GetResponseFromDao(ProductVariationDao productDao);
    IEnumerable<ProductVariationResponse> GetResponseFromDao(IEnumerable<ProductVariationDao> productVariationDao);
}
