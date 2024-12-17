using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IGiftCardsRepository
{
    Task<GiftCardDao> CreateAsync(GiftCardDao giftCardDao);
    Task<IEnumerable<GiftCardDao>> GetAllAsync();
    Task<GiftCardDao> GetAsync(Guid id);
    Task<GiftCardDao> GetAsyncByIdAndBusinessId(Guid id, Guid businessId);
    Task<GiftCardDao> GetAsyncByBusinessIdAndCode(Guid businessId, string code);
    Task<bool> ExistsAsyncWithCodeAndBusinessId(string code, Guid businessId);
    Task<bool> ExistsAsyncWithCodeAndBusinessIdExcludingId(string code, Guid businessId, Guid excludeId);
    Task<GiftCardDao> UpdateAsync(GiftCardDao giftCardDao);
}
