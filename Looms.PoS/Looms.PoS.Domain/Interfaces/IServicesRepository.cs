using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IServicesRepository
{
    Task<ServiceDao> CreateAsync(ServiceDao serviceDao);
    Task<IEnumerable<ServiceDao>> GetAllAsync();
    Task<ServiceDao> GetAsync(Guid id);
    void DeleteAsync(Guid id);
}
