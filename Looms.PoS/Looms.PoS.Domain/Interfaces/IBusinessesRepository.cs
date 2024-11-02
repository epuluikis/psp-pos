using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IBusinessesRepository
{
    Task<BusinessDao> CreateAsync(BusinessDao businessDao);
    IEnumerable<BusinessDao> GetAll();
    Task<BusinessDao> GetAsync(string id);
    void DeleteAsync(string id);
}
