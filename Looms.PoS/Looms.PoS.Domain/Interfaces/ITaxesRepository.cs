using Looms.PoS.Domain.Daos;
using Looms.PoS.Domain.Enums;

namespace Looms.PoS.Domain.Interfaces;

public interface ITaxesRepository
{
    Task<TaxDao> CreateAsync(TaxDao taxDao);
    Task<IEnumerable<TaxDao>> GetAllAsync();
    Task<TaxDao> GetAsync(Guid id);
    Task<TaxDao> GetByTaxCategoryAsync(TaxCategory taxCategory);
    Task<TaxDao> GetByTaxCategoryAndBusinessIdAsync(TaxCategory taxCategory, Guid businessId);
    Task<TaxDao> UpdateAsync(TaxDao taxDao);
}
