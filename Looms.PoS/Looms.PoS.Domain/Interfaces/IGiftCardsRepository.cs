using Looms.PoS.Domain.Daos;

namespace Looms.PoS.Domain.Interfaces;

public interface IGiftCardsRepository
{
    Task<GiftCardDao> CreateAsync(GiftCardDao giftCardDao);
    Task<IEnumerable<GiftCardDao>> GetAllAsync();
    Task<GiftCardDao> GetAsync(Guid id);
    Task<GiftCardDao> UpdateAsync(GiftCardDao giftCardDao);
}
