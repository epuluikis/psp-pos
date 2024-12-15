using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Application.Interfaces.Services;

public interface IRefundsTotalsService
{
    decimal CalculateRefundTotal(IEnumerable<RefundDao> refunds);
}