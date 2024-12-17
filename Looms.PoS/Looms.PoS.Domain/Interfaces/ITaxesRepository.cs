using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Domain.Interfaces;

public interface ITaxesRepository
{
    Task<TaxDao> CreateAsync(TaxDao taxDao);
    Task<IEnumerable<TaxDao>> GetAllAsync();
    Task<IEnumerable<TaxDao>> GetAllAsyncByBusinessId(Guid businessId);
    Task<TaxDao> GetAsync(Guid id);
    Task<TaxDao> GetAsyncByIdAndBusinessId(Guid id, Guid businessId);
    Task<TaxDao> GetByTaxCategoryAsync(TaxCategory taxCategory);
    Task<TaxDao> UpdateAsync(TaxDao taxDao);
}
