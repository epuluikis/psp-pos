using AutoMapper;
using Looms.PoS.Application.Interfaces.ModelsResolvers;
using Looms.PoS.Application.Models.Requests.Product;
using Looms.PoS.Application.Models.Responses.Product;
using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Mappings.ModelsResolvers;

public class ProductModelsResolver : IProductModelsResolver
{
    private readonly IMapper _mapper;

    public ProductModelsResolver(IMapper mapper)
    {
        _mapper = mapper;
    }

    public ProductDao GetDaoFromRequest(CreateProductRequest createProductRequest)
    {
        return _mapper.Map<ProductDao>(createProductRequest);
    }

    public ProductResponse GetResponseFromDao(ProductDao productDao)
    {
        return _mapper.Map<ProductResponse>(productDao);
    }

    public IEnumerable<ProductResponse> GetResponseFromDao(IEnumerable<ProductDao> productDao)
    {
        return _mapper.Map<IEnumerable<ProductResponse>>(productDao);
    }

    ProductDao IProductModelsResolver.GetDaoFromDaoAndRequest(ProductDao originalDao, UpdateProductRequest updateProductRequest)
    {
        return _mapper.Map<ProductDao>(updateProductRequest) with
        {
            Id = originalDao.Id,
            //TODO: idk what to do here, pls help
        };
    }

    ProductDao IProductModelsResolver.GetDeletedDao(ProductDao originalDao)
    {
        return originalDao with
        {
            IsDeleted = true
        };
    }
}
