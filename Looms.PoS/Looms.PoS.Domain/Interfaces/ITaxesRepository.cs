using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface ITaxesRepository
{
    Task<TaxDao> CreateAsync(TaxDao taxDao);
    Task<IEnumerable<TaxDao>> GetAllAsync();
    Task<TaxDao> GetAsync(Guid id);
    Task<TaxDao> UpdateAsync(TaxDao taxDao);
}
