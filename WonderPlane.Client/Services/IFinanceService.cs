using WonderPlane.Shared;

namespace WonderPlane.Client.Services
{
    public interface IFinanceService
    {
        Task<string> AddCard(CardDto cardDto);
        Task<List<CardDto>> GetCardsByUserId(int userId);
        Task<string> DeleteCardAsync(int cardId);
    }
}
