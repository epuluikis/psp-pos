using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IServicesRepository
{
    Task<ServiceDao> CreateAsync(ServiceDao serviceDao);
    Task<IEnumerable<ServiceDao>> GetAllAsync();
    Task<ServiceDao> GetAsync(Guid id);
    Task<ServiceDao> GetAsyncByIdAndBusinessId(Guid id, Guid businessId);
    Task<ServiceDao> UpdateAsync(ServiceDao serviceDao);
}
