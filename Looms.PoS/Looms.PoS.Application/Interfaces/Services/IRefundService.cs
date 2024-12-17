using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.Services;

public interface IRefundService
{
    decimal CalculateTotal(IEnumerable<RefundDao> refunds);
}
