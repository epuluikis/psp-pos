using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IBusinessesRepository
{
    Task<BusinessDao> CreateAsync(BusinessDao businessDao);
    Task<IEnumerable<BusinessDao>> GetAllAsync();
    Task<BusinessDao> GetAsync(Guid id);
    void DeleteAsync(Guid id);
}
