using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.Services;

public interface IRefundService
{
    decimal CalculateRefundTotal(IEnumerable<RefundDao> refunds);
}
